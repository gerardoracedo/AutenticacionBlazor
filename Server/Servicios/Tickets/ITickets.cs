using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Modelos.Tickets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Servicios.Tickets
{
    public interface ITickets
    {
        Task<MRespuestaBoolMensaje> InsertTicket(MTicketNuevo _v);
        Task<MRespuestaBoolMensaje> HabilitaTicket(int id_ticket, int id_ticket_detalle);
        Task<MRespuestaBoolMensaje> InsertTicketDetalle(MMsjNuevoUploadFiles _v);
        Task<MRespuestaBoolMensaje> InsertTicketPersonaJuridica(int id_ticket, int id_persona_juridica);
        Task<MRespuestaBoolMensaje> InsertTicketAltaActividad(int id_ticket, int id_persona_actividad);
        Task<MRespuestaBoolMensaje> Ticket_Acceso(int id_ticket);
        Task<IEnumerable<MTicketChat>> Ticket_Chat(int id_ticket);
        Task<IEnumerable<MTicketArchivos>> Ticket_Archivos(int id_ticket);
    }
}
