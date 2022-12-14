using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AutenticacionBlazor.Shared.Modelos.Global
{
    public class MTelefono
    {
        [Key]
        public int Id { get; set; }
        public int Uid_persona { get; set; }
        [Required(ErrorMessage = "Debe proveer una descripción")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Debe proveer un número telefónico")]
        public string Telefono { get; set; }
        public string Motivo_baja { get; set; }
        public string Usuario_graba { get; set; }
    }
}

