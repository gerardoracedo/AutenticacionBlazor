using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Global
{
    public class MVerificarLoginExterno
    {
        public int Uid_persona { get; set; }
        public string Identity { get; set; }
        public string Cuit { get; set; }
        public string Proveedor { get; set; }
        public string Provider_key { get; set; }
    }
}
