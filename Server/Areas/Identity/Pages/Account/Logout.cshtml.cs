using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AutenticacionBlazor.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using IdentityServer4;

namespace AutenticacionBlazor.Server.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var prop = new AuthenticationProperties()
            {
                RedirectUri = "/"
            };
            //var Url_1 = "http://tst.autenticar.gob.ar/auth/realms/yerbabuena-afip/protocol/openid-connect/logout?redirect_uri=";
            //var Url_2 = "http://tst.autenticar.gob.ar/auth/realms/yerbabuena-anses/protocol/openid-connect/logout?redirect_uri=";
            //var Url_3 = "http://tst.autenticar.gob.ar/auth/realms/yerbabuena-miarg/protocol/openid-connect/logout?redirect_uri=";
            //var Url_9 = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + "/authentication/logout";

            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync("Identity.External");
            await HttpContext.SignOutAsync("idsrv.external");
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("afip");
            await HttpContext.SignOutAsync("anses");
            await HttpContext.SignOutAsync("miarg");
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return Redirect("~/authentication/logout");
            //return Redirect(Url_1 + Url_2 + Url_3 + Url_9);
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            var Url_1 = "http://tst.autenticar.gob.ar/auth/realms/yerbabuena-afip/protocol/openid-connect/logout?redirect_uri=";
            var Url_2 = "http://tst.autenticar.gob.ar/auth/realms/yerbabuena-anses/protocol/openid-connect/logout?redirect_uri=";
            //var Url_3 = "http://tst.autenticar.gob.ar/auth/realms/yerbabuena-miarg/protocol/openid-connect/logout?redirect_uri=";
            var Url_9 = "~/authentication/logout";

            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync("afip");
            await HttpContext.SignOutAsync("anses");
            await HttpContext.SignOutAsync("miarg");
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            _logger.LogInformation("Usuario desconectado.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
