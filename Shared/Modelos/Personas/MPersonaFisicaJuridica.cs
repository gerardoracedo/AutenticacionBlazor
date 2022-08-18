using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Personas
{
    public class MPersonaFisicaJuridica
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public int Uid_persona_fisica { get; set; }
        public int Uid_persona_juridica { get; set; }
        public int Id_relacion { get; set; }
    }
}
