using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Rentas.Tesoreria
{
    public class MBoletas
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public string Nro_boleta { get; set; }
        public decimal Importe { get; set; }
        public DateTime Fecha_creacion { get; set; }
        public DateTime Fecha_vencimiento { get; set; }
        public DateTime Fecha_pago { get; set; }
        public DateTime Fecha_acreditacion { get; set; }
        public string Usuario_graba { get; set; }
        public int Uid_persona { get; set; }
    }
}
