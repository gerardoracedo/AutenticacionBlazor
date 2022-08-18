using AutenticacionBlazor.Server.Models;
using AutenticacionBlazor.Server.Servicios.ArchivosS3;
using AutenticacionBlazor.Server.Servicios.Global;
using AutenticacionBlazor.Server.Servicios.Personas;
using AutenticacionBlazor.Server.Servicios.Personas.Juridica;
using AutenticacionBlazor.Server.Servicios.Tickets;
using AutenticacionBlazor.Shared.Modelos;
using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Modelos.Personas;
using AutenticacionBlazor.Shared.Modelos.Personas.Juridica;
using AutenticacionBlazor.Shared.Modelos.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Controllers.Personas.Juridica
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaJuridicaController : Controller
    {
        private readonly IPersonaJuridica _personaJuridica;
        private readonly IArchivos _archivos;
        private readonly IGlobal _global;
        private readonly ITickets _tickets;
        private readonly IPersonas _personas;
        public PersonaJuridicaController(IPersonaJuridica personaJuridica, IArchivos archivos, IGlobal global, ITickets tickets, IPersonas personas)
        {
            _personaJuridica = personaJuridica;
            _archivos = archivos;
            _global = global;
            _tickets = tickets;
            _personas = personas;
        }

        /// <summary>
        /// Agregar una persona juridica
        /// </summary>
        [Authorize(Roles = "general")]
        [HttpPost("Agregar")]
        public async Task<MRespuestaBoolMensaje> AgregarPersonaJuridica(MPersonaJuridicaInsert personaJuridicaInsert)
        {
            MTicketNuevo _nticket = new MTicketNuevo();
            MRespuestaBoolMensaje _respuesta = new MRespuestaBoolMensaje();
            // Agregar los archivos (devuelve los id)
            var archivos = await _archivos.SubirArchivos2(personaJuridicaInsert.Archivos);
            // Agregar la persona (devuelve el id)
            var persona = await _personaJuridica.InsertPersonaJuridica(personaJuridicaInsert);
            // Crear el ticket (grabar el id de la persona y los id de archivos)
            _nticket.Estado = 2; // 2 = Esperando que termine de cargar todo
            _nticket.Id_tipo_ticket = 1; // 1 = Alta persona juridica (ver en tabla Archivos.Ticket_tipo)
            _nticket.Mensaje = "Inicia tramite de alta persona juridica";
            _nticket.Envia = 0; // 0 = inicia el tramite
            _nticket.Archivos = archivos;
            var ticket = await _tickets.InsertTicket(_nticket);
            var ticketPersonaJuridica = await _tickets.InsertTicketPersonaJuridica(ticket.id, persona.id);
            return persona;
        }

        /// <summary>
        /// Listar personas juridicas de una persona fisica (usuario logeado)
        /// </summary>
        [Authorize(Roles = "general,super,LecturaGeneral")]
        [HttpGet("Lista")]
        public async Task<MPersonaJuridicaGet> PersonaJuridicaLista()
        {
            return await _personaJuridica.GetListaPersonaJuridica();
        }

        /// <summary>
        /// Listar personas juridicas de una persona fisica (usuario logeado)
        /// </summary>
        [Authorize(Roles = "general,super,LecturaGeneral")]
        [HttpGet("ListaMisPersonasJuridicas")]
        public async Task<IEnumerable<MPersonaJuridicaGet>> PersonaJuridicaListaMisPersonasJuridicas()
        {
            return await _personaJuridica.GetGetListaPersonaJuridicaPersonaJuridica();
        }

        /// <summary>
        /// Lista de personas
        /// </summary>
        [Authorize(Roles = "general")]
        [HttpGet("Todos")]
        public async Task<IEnumerable<MPersonaJuridicaGet>> PersonaJuridicaTodos()
        {
            return await _personaJuridica.ListaPersonaJuridica();
        }

        /// <summary>
        /// Lista de relaciones
        /// </summary>
        [Authorize(Roles = "general")]
        [HttpGet("ListaRelaciones")]
        public async Task<IEnumerable<MRelacion>> ListaRelacion()
        {
            return await _personaJuridica.ListaRelacion();
        }

        /// <summary>
        /// Editar una persona juridica
        /// </summary>
        [Authorize(Roles = "general")]
        [HttpPost("Editar")]
        public async Task<MRespuestaBoolMensaje> EditarPersonaJuridica(MPersonaJuridicaUpdate personaJuridicaUpdate)
        {
            return await _personaJuridica.UpdatePersonaJuridica(personaJuridicaUpdate);
        }

        /// <summary>
        /// Lista de personas juridicas
        /// </summary>
        /// /// <remarks>
        /// Ejemplo id_identity:
        ///
        ///     f110fabe-40e5-4a31-aee1-721147e0027a
        ///
        /// </remarks>
        [Authorize(Roles = "general,super")]
        [HttpGet("Direccion/Lista")]
        public async Task<IEnumerable<MMisDirecciones>> ListaDireccionPersonasJuridicas()
        {
            return await _personas.ListaDireccionesPersonasJuridicas();
        }
    }
}
