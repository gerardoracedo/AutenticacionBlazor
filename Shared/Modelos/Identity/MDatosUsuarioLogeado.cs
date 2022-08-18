using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Models.Identity
{
    public class MDatosUsuarioLogeado
    {
        public bool Resultado { get; set; }
        public int Tipo_persona { get; set; }
        public int Uid { get; set; }
        public string Nombre_nombre_fantasia { get; set; }
        public string Apellido_nombre_legal { get; set; }
        public string Cuit { get; set; }
    }
}
