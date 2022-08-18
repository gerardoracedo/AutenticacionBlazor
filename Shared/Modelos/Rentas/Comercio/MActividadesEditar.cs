using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Rentas.Comercio
{
    public class MActividadesEditar
    {
        public int Id { get; set; }
        public int Codigo  { get; set; }

        [Required(ErrorMessage = "Escriba un nombre un nombre")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Ingrese una alicuota")]
        public string Alicuota { get; set; }

        [Required(ErrorMessage = "Ingrese una fecha")]
        public string Fecha_inicio { get; set; }
        public string Fecha_fin { get; set; }
        public int Exento { get; set; }
    }
}
