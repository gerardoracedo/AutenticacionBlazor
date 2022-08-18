using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AutenticacionBlazor.Shared.Modelos.Identity
{
    public class MAspNetRoles
    {
        [Key]
        public string RoleId { get; set; }
        [Required(ErrorMessage = "Debe proveer un nombre para el rol")]
        [MinLength(2)]
        public string RoleName { get; set; }
        public bool Status { get; set; }  //para saber si el rol está en el usuario
    }
}
