using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos
{
    public class MNuevo
    {
        public bool Resultado { get; set; }
        public string Mensaje { get; set; }
        public MNuevoPersona Respuesta { get; set; }
    }
    public class MNuevoPersona
    {
        public string Dni { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
    }
}
