using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Rentas.Comercio
{
    public class MListaGrupoActividades
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public string Tab { get; set; }
        public string Descripcion { get; set; }
    }
}
