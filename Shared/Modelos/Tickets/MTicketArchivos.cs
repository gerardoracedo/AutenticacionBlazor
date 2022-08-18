using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Tickets
{
    public class MTicketArchivos
    {
        public bool Acceso { get; set; }
        public int Id { get; set; }
        public int Estado { get; set; }
        public int Id_ticket_detalle { get; set; }
        public int Id_archivo { get; set; }
        public string Descargar { get; set; }
        public string Nombre { get; set; }
    }
}
