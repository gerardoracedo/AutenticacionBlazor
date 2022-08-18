using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Data.Entidades
{
    [Table("documento_persona", Schema = "Archivos")]
    public class Documento_Persona
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("idpersona")]
        public int IdPersona { get; set; }


        [Column("iddocumento")]
        public int IdDocumento { get; set; }

    }
}
