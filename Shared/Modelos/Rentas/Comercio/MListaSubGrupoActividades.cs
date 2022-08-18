using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Rentas.Comercio
{
    public class MListaSubGrupoActividades
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public int Id_titulo { get; set; }
        public int Grupo { get; set; }
        public string Descripcion { get; set; }
    }
}
