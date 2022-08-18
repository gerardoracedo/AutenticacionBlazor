using System;
using System.ComponentModel.DataAnnotations;

namespace AutenticacionBlazor.Shared.Modelos.Personas
{
    public class MInsertUpdateDirecciones
    {
        public int Id_direccion_persona { get; set; }
        public int Id_direccion { get; set; }
        [Required(ErrorMessage = "No se ingreso un identificador de persona")]
        public int Id_persona { get; set; }
        [Required(ErrorMessage = "No se ingreso un tipo de direccion")]
        [Range(1, 999999999, ErrorMessage = "No selecciono un tipo de direccion")]
        public int Id_tipo_direccion { get; set; }
        [Required(ErrorMessage = "No se ingreso un pais")]
        [Range(1, 999999999, ErrorMessage = "No selecciono un pais")]
        public int Id_pais { get; set; }
        [Required(ErrorMessage = "No se ingreso una provincia")]
        [Range(1, 999999999, ErrorMessage = "No selecciono una provincia")]
        public int Id_provincia { get; set; }
        [Required(ErrorMessage = "No se ingreso una localidad")]
        [Range(1, 999999999, ErrorMessage = "No selecciono una localidad")]
        public int Id_localidad { get; set; }
        [Required(ErrorMessage = "Ingrese un codigo postal")]
        public string Codigo_postal { get; set; }
        [Required(ErrorMessage = "Ingrese un nombre de calle")]
        public string Nombre_calle { get; set; }
        [Required(ErrorMessage = "Ingrese numero")]
        public string Numero { get; set; }
        public string Piso { get; set; }
        public string Departamento { get; set; }
        public string Sector { get; set; }
        public string Manzana { get; set; }
        public string Lote { get; set; }
        public string Descripcion { get; set; }
    }
}
