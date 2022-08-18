using AutenticacionBlazor.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using AutenticacionBlazor.Server.Servicios.Usuarios;
using AutenticacionBlazor.Shared.Models.Identity;

namespace AutenticacionBlazor.Server.Controllers.Identity
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacionController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUsuarios _iUsuarios;
        public AutenticacionController(
            UserManager<ApplicationUser> userManager, 
            IConfiguration configuration, 
            SignInManager<ApplicationUser> signInManager,
            IUsuarios iUsuarios)
        {
            this.userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _iUsuarios = iUsuarios;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string usuario, string contraseña)
        {
            if(usuario == null || contraseña == null)
            {
                return BadRequest("Ingrese un usuario y contraseña");
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(usuario, contraseña, false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return Ok(result);
                }
            }
            return Unauthorized();
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
        [HttpGet("UserData")]
        public async Task<MDatosUsuarioLogeado> UserData()
        {
            return await _iUsuarios.DatosUsuarioLogeado();
        }
    }
}
