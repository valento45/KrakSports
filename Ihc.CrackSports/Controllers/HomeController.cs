using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Claims;
using Ihc.CrackSports.Core.Objetos.Alunos;
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

        private readonly IAlunoService _alunoService;
        private readonly IClubService _clubService;

        public HomeController(ILogger<HomeController> logger, UserManager<Usuario> userManager, IAlunoService alunoService, IClubService clubService) : base(clubService, alunoService, userManager)
        {
            _logger = logger;
            _alunoService = alunoService;
            _clubService = clubService;
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
        public async Task<IActionResult> Login()
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
                if (await base.Autenticar(model))
                    return RedirectToAction("About");
                else
                    return View();

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
            await base.RefreshImageUser(User);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}