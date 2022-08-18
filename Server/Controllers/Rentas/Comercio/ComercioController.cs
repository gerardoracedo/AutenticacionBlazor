using AutenticacionBlazor.Server.Servicios.ArchivosS3;
using AutenticacionBlazor.Server.Servicios.Rentas.Comercio;
using AutenticacionBlazor.Server.Servicios.Tickets;
using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Modelos.Rentas.Comercio;
using AutenticacionBlazor.Shared.Modelos.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Controllers.Rentas.Comercio
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ComercioController : Controller
    {
        private readonly IComercioActividades _comercioActividades;
        private readonly ITickets _tickets;
        private readonly IArchivos _archivos;
        public ComercioController(IComercioActividades comercioActividades, ITickets tickets, IArchivos archivos)
        {
            _comercioActividades = comercioActividades;
            _tickets = tickets;
            _archivos = archivos;
        }

        /// <summary>
        /// Lista de actividades
        /// </summary>
        [Authorize(Roles = "general,super")]
        [HttpGet("Actividades/Lista/Activas")]
        public async Task<IEnumerable<MListaActividades>> ListaActividadesActivas()
        {
            return await _comercioActividades.ListaActividadesActivas();
        }

        /// <summary>
        /// Lista de grupo de actividades
        /// </summary>
        [Authorize(Roles = "general,super")]
        [HttpGet("Actividades/Lista/Grupos")]
        public async Task<IEnumerable<MListaGrupoActividades>> ListaGrupoActividades()
        {
            return await _comercioActividades.ListaActividadesGrupo();
        }

        /// <summary>
        /// Lista de sub-grupo de actividades
        /// </summary>
        [Authorize(Roles = "general,super")]
        [HttpGet("Actividades/Lista/SubGrupos")]
        public async Task<IEnumerable<MListaSubGrupoActividades>> ListaSubGrupoActividades()
        {
            return await _comercioActividades.ListaActividadesSubGrupo();
        }

        /// <summary>
        /// Agrega actividad a una persona
        /// </summary>
        /// /// <remarks>
        /// Ejemplo:
        ///
        ///     {
        ///        "id_identity": "f110fabe-40e5-4a31-aee1-721147e0027a",
        ///        "codigo_actividad": 11130,
        ///        "inicio_actividad": "2000-12-31",
        ///        "id_direccion": 1,
        ///        "id_titulo" : 19,
        ///        "id_subtitulo" : 165
        ///     }
        ///
        /// </remarks>
        [Authorize(Roles = "general,super")]
        [HttpPost("Actividades/Personas/Agregar")]
        public async Task<MRespuestaBoolMensaje> AgregarActividadesAPersonas(MAgregarActividadesAPersonas _v)
        {
            MTicketNuevo _nticket = new MTicketNuevo();
            MRespuestaBoolMensaje _respuesta = new MRespuestaBoolMensaje();
            // Agregar los archivos (devuelve los id)
            var archivos = await _archivos.SubirArchivos2(_v.Archivos);
            // Agregar la persona (devuelve el id)
            var actividad = await _comercioActividades.AgregarActividadesAPersona(_v);
            if(actividad.resultado == true)
            {
                // Crear el ticket (grabar el id de la persona y los id de archivos)
                _nticket.Estado = 2; // 2 = Esperando que termine de cargar todo
                _nticket.Id_tipo_ticket = 2; // (ver en tabla tickets.Ticket_tipo)
                _nticket.Mensaje = "Inicia tramite de agregar actividad";
                _nticket.Envia = 0; // 0 = inicia el tramite
                _nticket.Archivos = archivos;
                var ticket = await _tickets.InsertTicket(_nticket);
                var ticketCargar = await _tickets.InsertTicketAltaActividad(ticket.id, actividad.id);
            }
            
            return actividad;
        }

        /// <summary>
        /// Lista de actividades
        /// </summary>
        [Authorize(Roles = "general,super")]
        [HttpGet("Actividades/Persona/Listar")]
        public async Task<IEnumerable<MMisActividades>> MisActividades()
        {
            return await _comercioActividades.MisActividades();
        }

        [Authorize(Roles = "general,super")]
        [HttpGet("Actividades/Persona/Informacion")]
        public async Task<MDdjj_InformacionPersona> ComercioPersonaInformacion()
        {
            return await _comercioActividades.ComercioPersonaInformacion();
        }

        [Authorize(Roles = "general,super")]
        [HttpGet("Actividades/Persona/{periodo}")]
        public async Task<IEnumerable<MDdjj_actividades>> DdjjMisActividades(int periodo)
        {
            return await _comercioActividades.DdjjMisActividades(periodo);
        }

        [Authorize(Roles = "super")]
        [HttpPost("Actividades/Personas/Editar")]
        public async Task<MRespuestaBoolMensaje> EditarActividadesAPersonas(MActividadesEditar _v)
        {
            return await _comercioActividades.EditarActividadesAPersona(_v);
        }

        [Authorize(Roles = "general, super")]
        [HttpGet("ListadeSubtitulos/actividad/{id_titulo}")]
        public async Task<IEnumerable<MListaSubGrupoActividades>> ListaDeSubGruposPorIdGrupo(int id_titulo)
        {
            return await _comercioActividades.ListaSubGrupoByIdgrupo(id_titulo);
        }

        [Authorize(Roles = "general,super")]
        [HttpPost("Actividades/Agregar")]
        public async Task<MRespuestaBoolMensaje> AgregarActividades(MActividadesAlta _v)
        {
            return await _comercioActividades.Actividad_alta(_v);
        }

        [Authorize(Roles = "super")]
        [HttpPost("Actividades/Baja")]
        public async Task<MRespuestaBoolMensaje> BajaActividades(MActividadesBaja _v)
        {
            return await _comercioActividades.BajaActividades(_v);
        }

        [Authorize(Roles = "general")]
        [HttpPost("Ddjj/Agregar/Ver")]
        public async Task<MRespuestaBoolMensaje> Ddjj_agregar_ver(MDdjj_ver_carga _v)
        {
            return await _comercioActividades.Ddjj_agregar_ver(_v);
        }

        [Authorize(Roles = "general")]
        [HttpPost("Ddjj/Agregar")]
        public async Task<MRespuestaBoolMensaje> Agregarddjj(MDdjj_agregar _v)
        {
            return await _comercioActividades.Agregarddjj(_v);
        }
    }
}
