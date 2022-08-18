using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AutenticacionBlazor.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using IdentityModel;
using AutenticacionBlazor.Shared.Modelos;
using System.Net;
using System.Net.Sockets;
using AutenticacionBlazor.Server.Servicios;
using BlazorApp1.Server;
using Microsoft.Extensions.Options;
using AutenticacionBlazor.Shared.Modelos.Personas.Fisica;
using AutenticacionBlazor.Server.Servicios.Personas.Fisica;
using AutenticacionBlazor.Server.Servicios.Usuarios;
using AutenticacionBlazor.Shared.Modelos.Personas.Juridica;
using AutenticacionBlazor.Server.Servicios.Personas.Juridica;
using AutenticacionBlazor.Server.Servicios.Global;
using AutenticacionBlazor.Shared.Modelos.Global;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutenticacionBlazor.Server.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ExternalLoginModel> _logger;
        private readonly MPersonaFisicaExterna _mPersonaFisicaExterna = new MPersonaFisicaExterna();
        private readonly MPersonaJuridicaExterna _PersonaJuridicaExterna = new MPersonaJuridicaExterna();
        private readonly IPersonaFisica _iPersonaFisica;
        private readonly IPersonaJuridica _iPersonaJuridica;
        private readonly IUsuarios _iUsuarios;
        private readonly IGlobal _iGlobal;

        public ExternalLoginModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<ExternalLoginModel> logger,
            IEmailSender emailSender,
            IOptions<ConnectionConfig> connectionConfig,
            IPersonaFisica iPersonaFisica,
            IPersonaJuridica iPersonaJuridica,
            IUsuarios iUsuarios,
            IGlobal iGlobal)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _iPersonaFisica = iPersonaFisica;
            _iPersonaJuridica = iPersonaJuridica;
            _iUsuarios = iUsuarios;
            _iGlobal = iGlobal;

            var connection = connectionConfig.Value;
            string connectionString = connection.Analysis;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        [BindProperty]
        public InputModel2 Input2 { get; set; }

        public string ProviderDisplayName { get; set; }
        public string ReturnUrl { get; set; }
        public string xMail { get; set; }
        public string xCuit { get; set; }
        public string zProveedor { get; set; }
        public string xTipoPersona { get; set; }
        public IEnumerable<MListaPaises> listadepaises { get; set; }
        public IEnumerable<MListaGeneros> listadegeneros { get; set; }
        public IEnumerable<MListaEstadoCivil> listadeestadocivil { get; set; }
        public IEnumerable<MListaTipoDni> listadetipodni { get; set; }
        public IEnumerable<MListaTipoSociedad> listadetiposociedad { get; set; }
        public MVerificarLoginExterno _verificarLoginExterno { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Ingrese un Email")]
            [EmailAddress]
            public string Email { get; set; }
            [Required(ErrorMessage = "Ingrese una Fecha de nacimiento")]
            [DataType(DataType.Date)]
            public DateTime Fecha_nacimiento { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            [Range(1, 999999, ErrorMessage = "Seleccione un genero")]
            public int Id_genero { get; set; }
            [Range(1, 999999, ErrorMessage = "Seleccione un Pais")]
            public int Id_pais { get; set; }
            [Range(1, 999999, ErrorMessage = "Seleccione un Estado civil")]
            public int Id_estado_civil { get; set; }
            [Range(1, 999999, ErrorMessage = "Seleccione un Tipo de dni")]
            public int Id_tipo_dni { get; set; }
            [Range(1, 999999, ErrorMessage = "Seleccione un Tipo de sociedad")]
            public int Id_tipo_sociedad { get; set; }
        }

        public class InputModel2
        {
            public string Cuit { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Proveedor { get; set; }
            public string ApeyNomb { get; set; }
        }

        public async Task<IEnumerable<MListaPaises>> Lista_de_paises()
        {
            listadepaises = await _iGlobal.ListaPaises();
            return listadepaises;
        }

        public async Task<IEnumerable<MListaGeneros>> Lista_de_generos()
        {
            listadegeneros = await _iGlobal.ListaGeneros();
            return listadegeneros;
        }
        public async Task<IEnumerable<MListaEstadoCivil>> Lista_de_estado_civil()
        {
            listadeestadocivil = await _iGlobal.ListaEstadoCivil();
            return listadeestadocivil;
        }
        public async Task<IEnumerable<MListaTipoDni>> Lista_de_tipo_dni()
        {
            listadetipodni = await _iGlobal.ListaTipoDni();
            return listadetipodni;
        }

        public async Task<IEnumerable<MListaTipoSociedad>> Lista_de_tipo_sociedad()
        {
            listadetiposociedad = await _iGlobal.ListaTipoSociedad();
            return listadetiposociedad;
        }

        public async Task<MVerificarLoginExterno> VerificarLoginExterno(string cuit)
        {
            _verificarLoginExterno = await _iGlobal.VerificarLoginExterno(cuit);
            return _verificarLoginExterno;
        }

        public IActionResult OnGetAsync()
        {
            return RedirectToPage("./Login");
        }

        public IActionResult OnPost(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ErrorMessage = $"Error del proveedor externo: {remoteError}";
                return RedirectToPage("./Login", new {ReturnUrl = returnUrl });
            }

            /*
             * var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error al cargar la información de inicio de sesión externa.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }*/

            // Sign in the user with this external login provider if the user already has a login.
            //var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor : true);

            // Recibo datos
            var info = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
            if (info?.Succeeded != true)
            {
                ErrorMessage = "Error al cargar la información de inicio de sesión externa durante la confirmación.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Claims
            var xClaims = info.Principal.Claims.ToList();

            // Verifico el proveedor
            var xProveedor = xClaims.FirstOrDefault(x => x.Type == "proveedor");
            if (xProveedor == null)
            {
                xCuit = xClaims.FirstOrDefault(x => x.Type == "cuit").Value;
                xMail = "";
                zProveedor = "Anses";
                xTipoPersona = xClaims.FirstOrDefault(x => x.Type == "tipo_persona").Value;
            }
            else if (xProveedor.Value == "afip")
            {
                xCuit = xClaims.FirstOrDefault(x => x.Type == "cuit").Value;
                xMail = "";
                zProveedor = xProveedor.Value;
                xTipoPersona = xClaims.FirstOrDefault(x => x.Type == "tipo_persona").Value;
            }
            else if (xProveedor.Value == "miargentina")
            {
                xCuit = xClaims.FirstOrDefault(x => x.Type == "preferred_username").Value;
                xMail = xClaims.FirstOrDefault(x => x.Type == "email").Value;
                zProveedor = xProveedor.Value;
                xTipoPersona = "F";
            }
            else
            {
                xCuit = "";
                xMail = "";
                zProveedor = "";
                xTipoPersona = "";
            }

            // Datos de la persona
            var xUserid = xClaims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject);
            var xApellido = xClaims.FirstOrDefault(x => x.Type == "family_name");
            var xNombre = xClaims.FirstOrDefault(x => x.Type == "given_name");

            // Variables
            var oProveedor = zProveedor; // Proveedor (afip:afip / mi argentina:miargentina / anses:no envia)
            var oUserid = xUserid.Value; // sub del usuario (seria el id)
            var oApellido = xApellido.Value; // Apellido de la persona
            var oNombre = xNombre.Value; // Nombre de la persona
            var oNombrecompleto = oApellido + " " + oNombre; // Nombre completo de la persona
            var oMail = xMail;
            var oCuit = xCuit;
            var oTipoPersona = xTipoPersona; // F o FISICA, J o JURIDICA

            // Obtener datos con el cuit
            var _logExterno = await VerificarLoginExterno(oCuit);
            // Verificar logeo
            if (_logExterno.Provider_key != null)
            {
                if (_logExterno.Provider_key != oUserid || _logExterno.Proveedor != oProveedor)
                {
                    oProveedor = _logExterno.Proveedor;
                    oUserid = _logExterno.Provider_key;
                }
            }
            
            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(oProveedor, oUserid, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} iniciado sesión con {LoginProvider} proveedor.", oNombrecompleto, oProveedor);
                return LocalRedirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                //Lista de generos
                await Lista_de_generos();
                //Lista de paises
                await Lista_de_paises();
                //Lista de estado civil
                await Lista_de_estado_civil();
                //Lista de tipo dni
                await Lista_de_tipo_dni();
                //Lista de tipo sociedad
                await Lista_de_tipo_sociedad();

                // If the user does not have an account, then ask the user to create an account.
                ReturnUrl = returnUrl;
                ProviderDisplayName = oProveedor;
                /*if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    Input = new InputModel
                    {
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                    };
                }*/
                Input = new InputModel { Email = oMail };
                Input2 = new InputModel2 { Cuit = oCuit, Nombre = oNombre, Apellido = oApellido, Proveedor = oProveedor, ApeyNomb = oApellido+' '+ oNombre };
                return Page();
            }
        }
        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            // Get the information about the user from the external login provider
            /*var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error al cargar la información de inicio de sesión externa durante la confirmación.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }*/

            var info = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
            if (info?.Succeeded != true)
            {
                ErrorMessage = "Error al cargar la información de inicio de sesión externa durante la confirmación.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Claims
            var xClaims = info.Principal.Claims.ToList();

            // Verifico el proveedor
            var xProveedor = xClaims.FirstOrDefault(x => x.Type == "proveedor");
            if (xProveedor == null)
            {
                xCuit = xClaims.FirstOrDefault(x => x.Type == "cuit").Value;
                xMail = "";
                zProveedor = "Anses";
                xTipoPersona = xClaims.FirstOrDefault(x => x.Type == "tipo_persona").Value;
            }
            else if (xProveedor.Value == "afip")
            {
                xCuit = xClaims.FirstOrDefault(x => x.Type == "cuit").Value;
                xMail = "";
                zProveedor = xProveedor.Value;
                xTipoPersona = xClaims.FirstOrDefault(x => x.Type == "tipo_persona").Value;
            }
            else if (xProveedor.Value == "miargentina")
            {
                xCuit = xClaims.FirstOrDefault(x => x.Type == "preferred_username").Value;
                xMail = xClaims.FirstOrDefault(x => x.Type == "email").Value;
                zProveedor = xProveedor.Value;
                xTipoPersona = "F";
            }
            else
            {
                xCuit = "";
                xMail = "";
                zProveedor = "";
                xTipoPersona = "";
            }

            // Datos de la persona
            var xUserid = xClaims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject);
            var xApellido = xClaims.FirstOrDefault(x => x.Type == "family_name");
            var xNombre = xClaims.FirstOrDefault(x => x.Type == "given_name");

            // Variables
            var oProveedor = zProveedor; // Proveedor (afip:afip / mi argentina:miargentina / anses:no envia)
            var oUserid = xUserid.Value; // sub del usuario (seria el id)
            var oApellido = xApellido.Value; // Apellido de la persona
            var oNombre = xNombre.Value; // Nombre de la persona
            var oNombrecompleto = oApellido + " " + oNombre; // Nombre completo de la persona
            var oMail = xMail;
            var oCuit = xCuit;
            var oTipoPersona = xTipoPersona;

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var logininfo = new UserLoginInfo(oProveedor, oUserid, oNombrecompleto);

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, logininfo);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("El usuario creó una cuenta usando {Name} proveedor.", oProveedor);

                        var userId = await _userManager.GetUserIdAsync(user); // Id identity
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = userId, code = code },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Confirme su email",
                            $"Por faor confirme su cuenta <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>haciendo click aquí.</a>.");

                        // If account confirmation is required, we need to show the link if we don't have a real email sender
                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("./RegisterConfirmation", new { Email = Input.Email });
                        }

                        // Preparo datos de la persona
                        IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
                        IPAddress currentIpAddress = hostEntry.AddressList.FirstOrDefault(address => address.AddressFamily == AddressFamily.InterNetwork);

                        if (oTipoPersona == "F" || oTipoPersona == "FISICA")
                        {
                            // Persona Fisica
                            _mPersonaFisicaExterna.Id_tipo_persona = 1;
                            _mPersonaFisicaExterna.Apellido = oApellido;
                            _mPersonaFisicaExterna.Nombre = oNombre;
                            _mPersonaFisicaExterna.Dni = oCuit.Substring(2, 8);
                            _mPersonaFisicaExterna.Cuit = oCuit.Substring(0, 2) + "" + oCuit.Substring(2, 8) + "" + oCuit.Substring(10, 1);
                            //_mPersonaFisicaExterna.Fecha_nacimiento = new DateTime(1900, 1, 1);
                            _mPersonaFisicaExterna.Fecha_nacimiento = Input.Fecha_nacimiento;
                            _mPersonaFisicaExterna.Id_genero = Input.Id_genero;
                            _mPersonaFisicaExterna.Id_pais = Input.Id_pais;
                            _mPersonaFisicaExterna.Id_estado_civil = Input.Id_estado_civil;
                            _mPersonaFisicaExterna.Id_tipo_dni = Input.Id_tipo_dni;
                            _mPersonaFisicaExterna.Id_identity = userId;

                            var resultado = _iPersonaFisica.InsertPersonaFisicaExterna(_mPersonaFisicaExterna);
                        }
                        else if (oTipoPersona == "J" || oTipoPersona == "JURIDICA")
                        {
                            // Persona Juridica
                            _PersonaJuridicaExterna.Id_tipo_persona = 2;
                            _PersonaJuridicaExterna.Nombre_fantasia = "";
                            _PersonaJuridicaExterna.Nombre_legal = "";
                            _PersonaJuridicaExterna.Cuit = oCuit.Substring(0, 2) + "" + oCuit.Substring(2, 8) + "" + oCuit.Substring(10, 1); ;
                            _PersonaJuridicaExterna.Id_tipo_sociedad = Input.Id_tipo_sociedad;
                            _PersonaJuridicaExterna.Id_pais = Input.Id_pais;
                            _PersonaJuridicaExterna.Id_identity = userId;
                            var resultado = _iPersonaJuridica.InsertPersonaJuridicaExterna(_PersonaJuridicaExterna);
                        }
                        else
                        {
                            // Otra persona

                        }

                        // Asigno rol
                        _iUsuarios.AssignRoleToNewUser(userId);

                        // Logea al usuario
                        await _signInManager.SignInAsync(user, isPersistent: false, oProveedor);

                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ProviderDisplayName = oProveedor;
            ReturnUrl = returnUrl;
            return Page();
        }
    }
}
