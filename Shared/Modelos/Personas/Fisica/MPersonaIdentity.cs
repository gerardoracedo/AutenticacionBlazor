using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Personas.Fisica
{
    public class MPersonaIdentity
    {
        [Key]
        public int Id { get; set; }
        public short Estado { get; set; }
        public int Uid_persona { get; set; }
        public string Id_identity { get; set; }
        public DateTime? Fecha_graba { get; set; }
        public bool Resultado { get; set; }
        public string Mensaje { get; set; }

    }
}
