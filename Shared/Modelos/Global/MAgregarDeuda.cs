using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Global
{
    public class MAgregarDeuda
    {
        public int Estado { get; set; }
        public int Id_cuenta_corriente { get; set; }
        public string Importe { get; set; }
        public int Id_deuda_tipo { get; set; }
        public int Id_periodo { get; set; }
        public int Id_deuda_original { get; set; }
        public int Id_origen_deuda { get; set; }
        public string Usuario_graba { get; set; }
    }
}
