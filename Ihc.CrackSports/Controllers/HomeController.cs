using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Claims;
using Ihc.CrackSports.Core.Security;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.WebApp.Models;
using Ihc.CrackSports.WebApp.Models.Usuarios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;

namespace Ihc.CrackSports.WebApp.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Usuario> _userManager;
        private readonly IAlunoService alunoService;

        public HomeController(ILogger<HomeController> logger, UserManager<Usuario> userManager, IAlunoService alunoService) : base(alunoService)
        {
            _logger = logger;
            _userManager = userManager;
            this.alunoService = alunoService;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (User != null)
            {
                await base.RefreshImageUser(User);
            }

            return View();
        }

        [HttpGet]
        public  async Task<IActionResult> Login()
        {

            if (User != null)
            {
                return Redirect("Logout");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null)
                {

                    if (await _userManager.CheckPasswordAsync(user, Security.Encrypt(model.PasswordHash)))
                    {

                        var identity = new ClaimsIdentity("cookies");

                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                        identity.AddClaim(new Claim(ClaimTypes.Name, model.UserName));
                        identity.AddClaims(await _userManager.GetClaimsAsync(user));



                        if (identity.HasClaim(x => x.Value == Roles.ALUNO))
                        {
                            var aluno = await this.alunoService.GetByIdUsuario(user.Id);

                            if (!string.IsNullOrEmpty(aluno.FotoAlunoBase64))
                            {
                                identity.AddClaim(new Claim("Image", ""));
                                Roles.SetImage(aluno.FotoAlunoBase64);
                            }
                        }

                        var userClaim = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync("cookies", userClaim);                        

                        return RedirectToAction("About");
                    }
                }

            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync("cookies");
            return View("Login");
        }


        public IActionResult Privacy()
        {

            return View();
        }

        public async Task<IActionResult> About()
        {
            if (User != null)
            {
                await base.RefreshImageUser(User);
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}