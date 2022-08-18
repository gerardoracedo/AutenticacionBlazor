using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Personas.Juridica
{
    public class MPersonaJuridicaExterna
    {
        [Required(ErrorMessage = "Debe ingresar un tipo de persona")]
        public int Id_tipo_persona { get; set; }
        [Required(ErrorMessage = "Debe ingresar un Nombre de fantasia")]
        [MinLength(2)]
        public string Nombre_fantasia { get; set; }
        [Required(ErrorMessage = "Debe ingresar un Nombre legal")]
        [MinLength(2)]
        public string Nombre_legal { get; set; }
        [Required(ErrorMessage = "Debe ingresar un cuit")]
        public string Cuit { get; set; }
        [Required(ErrorMessage = "Debe ingresar un tipo de sociedad")]
        public int Id_tipo_sociedad { get; set; }
        [Required(ErrorMessage = "Debe ingresar pais")]
        public int Id_pais { get; set; }
        public string Id_identity { get; set; }
    }
}
