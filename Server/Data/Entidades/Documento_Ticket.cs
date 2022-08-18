using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Data.Entidades
{
    [Table("documento_ticket", Schema = "Archivos")]
    public class Documento_Ticket
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("iddocumento")]
        public int IdDocumento { get; set; }

        [Column("idticket")]
        public int IdTicket { get; set; }

    }

}
