using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutenticacionBlazor.Server.Data;
using AutenticacionBlazor.Server.Models;
using BlazorApp1.Server;
using Blazored.Modal;
using AutenticacionBlazor.Server.Helpers;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Blazored.LocalStorage;
using AutenticacionBlazor.Shared.Servicios;
using AutenticacionBlazor.Shared.Modelos;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using AutenticacionBlazor.Server.Servicios.Personas.Fisica;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutenticacionBlazor.Server.Servicios.Usuarios;
using AutenticacionBlazor.Server.Servicios.Personas.Juridica;
using AutenticacionBlazor.Server.Servicios.Global;
using System;
using AutenticacionBlazor.Server.Servicios.Personas;
using AutenticacionBlazor.Server.Servicios.Rentas.Comercio;
using System.Reflection;
using System.IO;
using AutenticacionBlazor.Server.Servicios;
using AutenticacionBlazor.Server.Servicios.ArchivosS3;
using Amazon.S3;
using AutenticacionBlazor.Server.Servicios.Tickets;
using AutenticacionBlazor.Server.Servicios.Tesoreria;

namespace AutenticacionBlazor.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }    
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Conexion SQL
            var postgreSQLyerbabuena = new PostgreSQLConfiguration(Configuration.GetConnectionString("PostgreSql_DB_yerbabuena"), Configuration.GetConnectionString("DataAccessPostgreSqlProvider"));
            services.AddSingleton(postgreSQLyerbabuena);
            var sqlConnectionString = Configuration.GetConnectionString("DataAccessPostgreSqlProvider");
            services.Configure<ConnectionConfig>(Configuration.GetSection("DataAccessPostgreSqlProvider"));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    sqlConnectionString
                )
            );

            services.AddControllers();
            
            // Swagger
            /*services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Municipalidad de Yerba Buena - Web API",
                    Description = ""
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);
            });
            // Mapper (prueba)
            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new MappingProfile());
            });*/

            

            services.AddScoped<UsuarioLogeado>();
            services.AddScoped<IPersonaFisica, SPersonaFisica>();
            services.AddScoped<IPersonaJuridica, SPersonaJuridica>();
            services.AddScoped<IUsuarios, SUsuarios>();
            services.AddScoped<IGlobal, SGlobal>();
            services.AddScoped<IPersonas, SPersonas>();
            services.AddTransient<IComercioActividades, SComercioActividades>();
            services.AddScoped<IServiciosPersonaArchivos, ServiciosPersonaArchivos>();
            services.AddScoped<IServicioTicketArchivos, ServicioTicketArchivos>();
            services.AddScoped<IArchivos, SArchivos>();
            services.AddScoped<ITickets, STickets>();
            services.AddTransient<ITesoreria, STesoreria>();

            //IMapper mapper = mapperConfig.CreateMapper();
            //services.AddSingleton(mapper);
            services.AddMvc();

            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();

            // Use a PostgreSQL database
            

            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();

            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();

            

            services.AddDefaultIdentity<ApplicationUser>(options => {
                                                            options.SignIn.RequireConfirmedAccount = false;
                                                            options.Password.RequireDigit = false;
                                                            options.Password.RequireLowercase = false;
                                                            options.Password.RequireUppercase = false;
                                                            options.Password.RequireNonAlphanumeric = false;
                                                            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>()
            .AddProfileService<IdentityProfileService>();
            services.AddApiAuthorization().AddAccountClaimsPrincipalFactory<RolesClaimsPrincipalFactory>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddBlazoredModal();
            services.AddBlazoredLocalStorage();
            //services.AddScoped<DialogService>();
            //services.AddScoped<NotificationService>();
            //services.AddScoped<TooltipService>();

            services.AddHttpContextAccessor();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
                .AddCookie()
                .AddOpenIdConnect("afip", "AFIP", options =>
                {
                    options.SignInScheme = IdentityConstants.ExternalScheme;
                    options.Authority = Configuration["Authority_1"];
                    options.ClientId = Configuration["ClientId"];
                    options.ClientSecret = Configuration["ClientSecret_1"];
                    options.ResponseType = "code id_token";
                    options.CallbackPath = new PathString("/signin-oidc/a");
                    options.SaveTokens = true;
                    options.TokenValidationParameters = new TokenValidationParameters(){ NameClaimType = "name" };
                })
                .AddOpenIdConnect("anses", "Anses", options =>
                {
                    options.SignInScheme = IdentityConstants.ExternalScheme;
                    options.Authority = Configuration["Authority_2"];
                    options.ClientId = Configuration["ClientId"];
                    options.ClientSecret = Configuration["ClientSecret_2"];
                    options.ResponseType = "code id_token";
                    options.CallbackPath = new PathString("/signin-oidc/b");
                    options.SaveTokens = true;
                    options.TokenValidationParameters = new TokenValidationParameters() { NameClaimType = "name" };
                })
                .AddOpenIdConnect("miarg", "Mi Argentina", options =>
                {
                    options.SignInScheme = IdentityConstants.ExternalScheme;
                    options.Authority = Configuration["Authority_3"];
                    options.ClientId = Configuration["ClientId"];
                    options.ClientSecret = Configuration["ClientSecret_3"];
                    options.ResponseType = "code id_token";
                    options.CallbackPath = new PathString("/signin-oidc/c");
                    options.SaveTokens = true;
                    options.TokenValidationParameters = new TokenValidationParameters() { NameClaimType = "name" };
                })
                ;

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Accessdenied";
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            /*app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.DefaultModelsExpandDepth(-1);
            });*/

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
