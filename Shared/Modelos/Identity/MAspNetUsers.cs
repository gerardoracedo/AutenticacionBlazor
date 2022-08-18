using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using AutenticacionBlazor.Shared.Modelos.Global;

namespace AutenticacionBlazor.Shared.Modelos.Identity
{
    public class MAspNetUsers
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public List<MAspNetUserRoles> UserRoles { get; set; } = new List<MAspNetUserRoles>();
        public List<MUserOficinas> UserOficinas { get; set; } = new List<MUserOficinas>();
    }
}
