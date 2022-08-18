using AutenticacionBlazor.Shared.Modelos.Identity;
using AutenticacionBlazor.Shared.Modelos.Global;
using AutenticacionBlazor.Shared.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Servicios.Usuarios
{
    public interface IUsuarios
    {
        Task<IEnumerable<MAspNetUsers>> GetAspNetUsers(); 
        Task<IEnumerable<MAspNetRoles>> GetAspNetRoles();
        Task<IEnumerable<MAspNetUsers>> SearchUsers(string term);
        Task<IEnumerable<MAspNetRoles>> SearchRoles(string term);
        Task<MAspNetRoles> GetRole(string roleid);
        Task<MRespuestaBoolMensaje> InsertRole(MAspNetRoles aspnetrole);
        Task<MRespuestaBoolMensaje> UpdateRole(MAspNetRoles aspnetrole);
        Task<MRespuestaBoolMensaje> DeleteRole(MAspNetRoles aspnetrole);
        void AssignRoleToNewUser(string ididentity);
        Task<MRespuestaBoolMensaje> AssignRoleToUser(string roleid, MAspNetUsers aspnetuser);
        Task<MRespuestaBoolMensaje> RemoveRoleFromUser(string roleid, MAspNetUsers aspnetuser);
        Task<IEnumerable<MAspNetRoles>> GetUserRoles(string ididentity);
        Task<IEnumerable<MAspNetRoles>> GetNotUserRoles(string ididentity);
        void AsignarRolAUsuario(string mail);
        Task<MDatosUsuarioLogeado> DatosUsuarioLogeado();
    }
}
