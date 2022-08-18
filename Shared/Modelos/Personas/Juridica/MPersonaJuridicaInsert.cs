using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutenticacionBlazor.Shared.Helpers;
using AutenticacionBlazor.Shared.Modelos.Global;

namespace AutenticacionBlazor.Shared.Modelos.Personas.Juridica
{
    public class MPersonaJuridicaInsert
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe ingresar un tipo de persona")]
        public int Id_tipo_persona { get; set; }
        [Required(ErrorMessage = "Debe ingresar un Nombre de fantasia")]
        [MinLength(2)]
        public string Nombre_fantasia { get; set; }
        [Required(ErrorMessage = "Debe ingresar un Nombre legal")]
        [MinLength(2)]
        public string Nombre_legal { get; set; }
        [CuilValidator]
        [Required(ErrorMessage = "Debe ingresar un cuit")]
        public string Cuit { get; set; }
        [Required(ErrorMessage = "Debe ingresar un tipo de sociedad")]
        [Range(1, 9999999, ErrorMessage = "Debe ingresar/seleccionar un tipo de sociedad")]
        public int Id_tipo_sociedad { get; set; }
        [Required(ErrorMessage = "Debe ingresar pais")]
        [Range(1, 9999999, ErrorMessage = "Debe ingresar/seleccionar un pais")]
        public int Id_pais { get; set; }
        public int Uid_persona_fisica { get; set; }
        public string Rol { get; set; }
        public int Estado { get; set; }
        public List<UploadedFile> Archivos { get; set; } = new List<UploadedFile>();
    }
}
