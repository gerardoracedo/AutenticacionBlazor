using AutenticacionBlazor.Server.Servicios.Usuarios;
using AutenticacionBlazor.Shared.Modelos.Identity;
using AutenticacionBlazor.Shared.Modelos.Global;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Controllers.Identity
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUsuarios _user;

        public UserController(IUsuarios user)
        {
            _user = user;
        }

        [Authorize(Roles = "admin, super")]
        [HttpGet("GetAspNetUsers")]
        public async Task<IEnumerable<MAspNetUsers>> GetAspNetUsers()
        {
            return await _user.GetAspNetUsers();
        }

        [Authorize(Roles = "admin, super")]
        [HttpGet("GetAspNetRoles")]
        public async Task<IEnumerable<MAspNetRoles>> GetAspNetRoles()
        {
            return await _user.GetAspNetRoles();
        }

        [Authorize(Roles = "admin, super")]
        [HttpGet("GetRole/{roleid}")]
        public async Task<MAspNetRoles> GetRole(string roleid)
        {
            return await _user.GetRole(roleid);
        }

        [Authorize(Roles = "admin, super")]
        [HttpPost("InsertRole")]
        public async Task<MRespuestaBoolMensaje> InsertRole(MAspNetRoles aspnetrole)
        {
            return await _user.InsertRole(aspnetrole);
        }

        [Authorize(Roles = "admin, super")]
        [HttpPost("UpdateRole")]
        public async Task<MRespuestaBoolMensaje> UpdateRole(MAspNetRoles aspnetrole)
        {
            return await _user.UpdateRole(aspnetrole);
        }

        [Authorize(Roles = "admin, super")]
        [HttpPost("DeleteRole")]
        public async Task<MRespuestaBoolMensaje> DeleteRole(MAspNetRoles aspnetrole)
        {
            return await _user.DeleteRole(aspnetrole);
        }

        [Authorize(Roles = "admin, super")]
        [HttpGet("SearchUsers/{term}")]
        public async Task<IEnumerable<MAspNetUsers>> SearchUsers(string term)
        {
            return await _user.SearchUsers(term);
        }

        [Authorize(Roles = "admin, super")]
        [HttpGet("SearchRoles/{term}")]
        public async Task<IEnumerable<MAspNetRoles>> SearchRoles(string term)
        {
            return await _user.SearchRoles(term);
        }

        [Authorize(Roles = "admin, super")]
        [HttpPost("AssignRoleToUser/{roleid}")]
        public async Task<MRespuestaBoolMensaje> AssignRoleToUser(string roleid, MAspNetUsers aspnetuser)
        {
            return await _user.AssignRoleToUser(roleid, aspnetuser);
        }

        [Authorize(Roles = "admin, super")]
        [HttpPost("RemoveRoleFromUser/{roleid}")]
        public async Task<MRespuestaBoolMensaje> RemoveRoleFromUser(string roleid, MAspNetUsers aspnetuser)
        {
            return await _user.RemoveRoleFromUser(roleid, aspnetuser);
        }

        [Authorize(Roles = "admin, super")]
        [HttpGet("GetUserRoles/{ididentity}")]
        public async Task<IEnumerable<MAspNetRoles>> GetUserRoles(string ididentity)
        {
            return await _user.GetUserRoles(ididentity);
        }

        [Authorize(Roles = "admin, super")]
        [HttpGet("GetNotUserRoles/{ididentity}")]
        public async Task<IEnumerable<MAspNetRoles>> GetNotUserRoles(string ididentity)
        {
            return await _user.GetNotUserRoles(ididentity);
        }

    }
}
