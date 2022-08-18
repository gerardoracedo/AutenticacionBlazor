using AutenticacionBlazor.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Personas.Fisica
{
    public class MPersonaFisicaLista
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public int Uid_persona { get; set; }
        public string Id_identity { get; set; }
        [Required(ErrorMessage = "Debe ingresar un apellido")]
        [MinLength(2)]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [MinLength(2)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe ingresar un dni")]
        [MinLength(8)]
        [Range(00000000, 99999999)]
        public string Dni { get; set; }
        [CuilValidator]
        [Required(ErrorMessage = "Debe ingresar un cuit")]
        public string Cuit { get; set; }
        //[DateValidator]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Debe ingresar la fecha de nacimiento")]
        public string Fecha_nacimiento { get; set; }
        [Required(ErrorMessage = "Debe ingresar un género")]
        [Range(1, 999999, ErrorMessage = "Seleccione un género")]
        public int Id_genero { get; set; }
        [Required(ErrorMessage = "Debe ingresar un país")]
        [Range(1, 999999, ErrorMessage = "Seleccione un Pais")]
        public int Id_pais { get; set; }
        [Required(ErrorMessage = "Debe ingresar un estado civil")]
        [Range(1, 999999, ErrorMessage = "Seleccione un estado civil")]
        public int Id_estado_civil { get; set; }
        [Required(ErrorMessage = "Debe ingresar un tipo de dni")]
        [Range(1, 999999, ErrorMessage = "Seleccione un tipo de dni")]
        public int Id_tipo_dni { get; set; }
        public string Genero { get; set; }
        public string Pais { get; set; }
        public string Estado_civil { get; set; }
        public string Tipo_dni { get; set; }
    }
}
