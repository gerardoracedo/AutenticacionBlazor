using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Data.Entidades
{
    [Table("documentos", Schema = "Archivos")]
    public class Documento
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("extencion")]
        public string Extencion { get; set; }

        [Column("nombrearchivo")]
        public string NombreArchivo { get; set; }

        public virtual ICollection<PersonaEntidad> Personas { get; set; }

    }

}
