using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Rentas.Tesoreria
{
    public class MBoletasCodigosBarras
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public int Id_boleta { get; set; }
        public string Codigo_barra { get; set; }
        public string Tipo_codigo_barra { get; set; }
    }
}