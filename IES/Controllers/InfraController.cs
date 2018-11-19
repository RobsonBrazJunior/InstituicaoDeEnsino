using System.Threading.Tasks;
using IES.Models.Infra;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IES.Controllers
{
    [Authorize]
    public class InfraController : Controller
    {
        private readonly UserManager<UsuarioDaAplicacao> _userManager;
        private readonly SignInManager<UsuarioDaAplicacao> _signInManager;
        private readonly ILogger _logger;

        public InfraController(
            UserManager<UsuarioDaAplicacao> userManager,
            SignInManager<UsuarioDaAplicacao> signInManager,
            ILogger<InfraController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Acessar(string returnUrl)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
    }
}