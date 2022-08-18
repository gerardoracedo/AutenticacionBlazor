using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Rentas.Tesoreria
{
    public class MBoletasDetalle
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public int Id_boleta { get; set; }
        public int Id_deuda { get; set; }
        public string Descripcion { get; set; }
        public decimal Importe { get; set; }
        public int Id_periodo_fiscal { get; set; }
        public int Id_tipo_deuda { get; set; }
        public DateTime Fecha_graba { get; set; }
    }
}
