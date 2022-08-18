using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Tickets
{
    public class MTicketChat
    {
        public bool Acceso { get; set; }
        public int Id { get; set; }
        public int Estado { get; set; }
        public int Id_ticket { get; set; }
        public string Mensaje { get; set; }
        public string Usuario { get; set; }
        public string Fecha_graba { get; set; }
        public int Envia { get; set; }
    }
}
