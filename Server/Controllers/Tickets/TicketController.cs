using AutenticacionBlazor.Server.Servicios.ArchivosS3;
using AutenticacionBlazor.Server.Servicios.Tickets;
using AutenticacionBlazor.Shared.Modelos;
using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Modelos.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Controllers.Tickets
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : Controller
    {
        private readonly ITickets _tickets;
        private readonly IArchivos _archivos;
        public TicketController(ITickets tickets, IArchivos archivos)
        {
            _tickets = tickets;
            _archivos = archivos;
        }

        /// <summary>
        /// Verificar si el usuario puede ver el tramite
        /// </summary>
        [Authorize(Roles = "general, super, admin")]
        [HttpGet("Tramite/Acceso/{_idTramite}")]
        public async Task<MRespuestaBoolMensaje> VerificarAccesoTicket(int _idTramite)
        {
            return await _tickets.Ticket_Acceso(_idTramite);
        }

        /// <summary>
        /// -
        /// </summary>
        [Authorize(Roles = "general, super, admin")]
        [HttpGet("Tramite/Info/{_idTramite}")]
        public async Task<IEnumerable<MTicketChat>> Ticket_Chat(int _idTramite)
        {
            return await _tickets.Ticket_Chat(_idTramite);
        }

        /// <summary>
        /// -
        /// </summary>
        [Authorize(Roles = "general, super, admin")]
        [HttpGet("Tramite/Archivos/{_idTramite}")]
        public async Task<IEnumerable<MTicketArchivos>> Ticket_Archivos(int _idTramite)
        {
            return await _tickets.Ticket_Archivos(_idTramite);
        }

        /// <summary>
        /// -
        /// </summary>
        [Authorize(Roles = "general, super, admin")]
        [HttpPost("Tramite/InsertMsj")]
        public async Task<MRespuestaBoolMensaje> Ticket_Insert(MMsjNuevoUploadFiles _msj)
        {
            var list = new List<UploadedFile>();
            foreach (var item in _msj.ArchivosGuardar)
            {
                var o = new UploadedFile()
                {
                    FileContent = item.FileContent,
                    FileName= item.FileName,
                    FileType = item.FileType
                }; 
                list.Add(o);
            }
            var listaRespuestaArchivos = await _archivos.SubirArchivos2(_msj.ArchivosGuardar);
            _msj.ArchivosRespuesta = listaRespuestaArchivos;
            return await _tickets.InsertTicketDetalle(_msj);

        }
    }
}
