using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Servicios
{
    public class UsuarioLogeado
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsuarioLogeado(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string IdUsuarioIdentity()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var _Claims = _httpContextAccessor.HttpContext.User.Identities.First().Claims.ToList();
                return _Claims.FirstOrDefault(x => x.Type == "sub").Value;
            }
            return "";
        }
        public int CheckUserRol(string _rol)
        {
            // return 0 no tiene ese rol
            // return 1 tiene el rol
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var _AdminRole = _httpContextAccessor.HttpContext.User.IsInRole(_rol);
                if (_AdminRole)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }
    }
}
