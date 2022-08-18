using System;
using System.ComponentModel.DataAnnotations;

namespace AutenticacionBlazor.Shared.Modelos.Rentas.Comercio
{
    public class MActividadesAlta
    {
        public int Estado { get; set; }

        [Required(ErrorMessage = "Debe ingresar Un tipo de actividad")]
        [Range(1, 9999999999, ErrorMessage = "Debe ingresar Un tipo de actividad")]
        public int Id_subtitulo { get; set; }

        [Required(ErrorMessage = "Debe ingresar Codigo de la actividad")]
        [Range(1, 9999999999, ErrorMessage = "Ingresar Codigo de la actividad, debe ser un numero entero")]
        public int Codigo { get; set; }
        [Required(ErrorMessage = "Debe ingresar descripcion de la actividad")]
        public string Descripcion { get; set; }
        public string Observacion { get; set; }
        [Required(ErrorMessage = "Debe ingresar una Alicuota")]
        public string Alicuota { get; set; }
        public string Fecha_inicio { get; set; }
        public string Fecha_fin { get; set; }
        public int Exento { get; set; }
    }
}
