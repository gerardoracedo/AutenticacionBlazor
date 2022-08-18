using AutenticacionBlazor.Server.Data.Entidades;
using AutenticacionBlazor.Server.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Data
{    
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    //public class ApplicationDbContext : ApiAuthorizationDbContext<IdentityUser>
    { 
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Documento_Persona> Documento_Personas { get; set; }
        public DbSet<Documento_Ticket> Documento_Tickets { get; set; }
        public DbSet<PersonaEntidad> Personas { get; set; }

    }
}
