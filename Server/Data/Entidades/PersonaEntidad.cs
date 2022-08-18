using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Data.Entidades
{
    [Table("Persona", Schema = "personas")]
    public class PersonaEntidad
    {
        [Key]
        [Column("Uid")]
        public int UId { get; set; }

        [Column("Id_tipo_persona")]
        public int IdTipoPersona { get; set; }

        [Column("Estado")]
        public int Status { get; set; }

        public virtual ICollection<Documento> Documentos { get; set; }

    }
}
