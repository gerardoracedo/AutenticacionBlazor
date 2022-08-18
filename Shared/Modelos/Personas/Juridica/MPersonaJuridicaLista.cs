using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Personas.Juridica
{
    public class MPersonaJuridicaLista
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public int Uid_persona { get; set; }
        public string Id_identity { get; set; }
        public string Nombre_fantasia { get; set; }
        public string Nombre_legal { get; set; }
        public string Cuit { get; set; }
        public int Id_tipo_sociedad { get; set; }
        public string Tipo_sociedad { get; set; }
        public int Id_pais { get; set; }
        public string Pais { get; set; }
    }
}
