using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AutenticacionBlazor.Shared.Modelos.Global
{
    public class MUserOficinas
    {
        public string Id_Identity { get; set; }
        public int Id_Oficina { get; set; }
        public bool Status { get; set; }  //para saber si la oficina está en el usuario
    }
}
