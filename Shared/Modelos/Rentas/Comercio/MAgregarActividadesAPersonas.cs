using AutenticacionBlazor.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Rentas.Comercio
{
    public class MAgregarActividadesAPersonas
    {
        public string Id_identity { get; set; }
        [Required(ErrorMessage = "Seleccione una persona")]
        [Range(1, 9999999, ErrorMessage = "Seleccione una persona")]
        public int Uid_persona { get; set; }
        [Required(ErrorMessage = "Debe ingresar/seleccionar un codigo de actividad")]
        [Range(1, 9999999, ErrorMessage = "Debe ingresar/seleccionar un codigo de actividad")]
        public int Codigo_actividad { get; set; }
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Inicio de actividad incorrecto")]
        [Required(ErrorMessage = "Inicio de actividad incorrecto")]
        public string Inicio_actividad { get; set; }
        [Required(ErrorMessage = "Debe ingresar/seleccionar una direccion")]
        [Range(1, 9999999, ErrorMessage = "Debe ingresar/seleccionar una direccion")]
        public int Id_direccion { get; set; }
        
        public int Id_titulo { get; set; }
        
        public int Id_subtitulo { get; set; }
        public bool Local { get; set; }
        public string Nombre_local { get; set; }
        public int Padron_cisi { get; set; }
        public List<UploadedFile> Archivos { get; set; } = new List<UploadedFile>();
    }
}
