using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AutenticacionBlazor.Shared.Modelos.Global
{
    public class MOficina
    {
        [Key]
        public int Id_Oficina { get; set; }
        public int Estado { get; set; }
        public string Oficina { get; set; }
        public int Depende { get; set; }
        public string Codigo { get; set; }
        public int Pilar { get; set; }
        public bool Status { get; set; }
    }
}

