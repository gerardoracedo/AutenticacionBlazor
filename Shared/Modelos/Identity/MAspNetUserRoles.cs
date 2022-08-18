using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AutenticacionBlazor.Shared.Modelos.Identity
{
    public class MAspNetUserRoles
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
