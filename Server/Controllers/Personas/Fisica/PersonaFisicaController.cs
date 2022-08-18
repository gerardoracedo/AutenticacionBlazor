using AutenticacionBlazor.Server.Servicios.Personas.Fisica;
using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Modelos.Identity;
using AutenticacionBlazor.Shared.Modelos.Personas.Fisica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Controllers.Personas.Fisica
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaFisicaController : Controller
    {
        private readonly IPersonaFisica _personaFisica;

        public PersonaFisicaController(IPersonaFisica personaFisica)
        {
            _personaFisica = personaFisica;
        }
        /// <summary>
        /// Agregar una persona fisica
        /// </summary>
        [Authorize(Roles = "super, admin, AgregarPersonaFisica")]
        [HttpPost("Agregar")]
        public async Task<MRespuestaBoolMensaje> AgregarPersonaFisica(MPersonaFisicaInsert personaFisicaInsert)
        {
            return await _personaFisica.InsertPersonaFisica(personaFisicaInsert);
        }
        /// <summary>
        /// Obtener los datos del usuario logeado
        /// </summary>
        [HttpGet("Informacion")]
        public async Task<MPersonaFisicaGet> InformacionPersonaFisica()
        {
            return await _personaFisica.GetPersonaFisica();
        }

        /// <summary>
        /// Lista de personas
        /// </summary>
        [Authorize(Roles = "super, admin")]
        [HttpGet("Lista")]
        public async Task<IActionResult> ListaPersonaFisica()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(await _personaFisica.ListaPersonaFisica());
            }
            return Unauthorized();
        }

        [Authorize(Roles = "super, admin, EditarPersonaFisica")]
        [HttpPost("UpdatePersonaFisica")]
        public async Task<MRespuestaBoolMensaje> UpdatePersonaFisica(MPersonaFisicaLista pfu)
        {
            return await _personaFisica.UpdatePersonaFisica(pfu);
        }


        [Authorize(Roles = "super, admin")]
        [HttpGet("GetPersonasFisicas")]
        public async Task<IActionResult> GetPersonasFisicas()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(await _personaFisica.GetPersonasFisicas());
            }
            return Unauthorized();
        }

        [Authorize(Roles = "admin, super")]
        [HttpGet("GetUid")]
        public async Task<MObtenerUidPersona> GetUid()
        {
            return await _personaFisica.GetUid();
        }

        [Authorize(Roles = "super, admin")]
        [HttpGet("SearchPersonaFisica/{term}")]
        public async Task<IEnumerable<MPersonaFisicaLista>> SearchPersonaFisica(string term)
        {
            return await _personaFisica.SearchPersonaFisica(term);
        }

        [Authorize(Roles = "admin, super")]
        [HttpGet("GetPersonaFisicaById/{id}")]
        public async Task<MPersonaFisicaGet> GetPersonaFisicaById(int id)
        {
            return await _personaFisica.GetPersonaFisicaById(id);
        }

    }
}
