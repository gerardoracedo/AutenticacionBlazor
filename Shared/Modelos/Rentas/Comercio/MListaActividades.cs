using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Rentas.Comercio
{
    public class MListaActividades
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public int Id_subtitulos { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Alicuota { get; set; }
        public string Fecha_inicio { get; set; }
        public string Fecha_fin { get; set; }
        public int Excento { get; set; }
    }
}
