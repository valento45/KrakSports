﻿using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Claims;
using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Commands;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Notifications.Hubs;
using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Security;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.Core.ViewModel.Home;
using Ihc.CrackSports.WebApp.Application.Interfaces;
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
        private readonly IColaboradorService _colaboradorService;
        private readonly ICEPService _cepService;


        public HomeController(ILogger<HomeController> logger, UserManager<Usuario> userManager, IAlunoService alunoService, IClubService clubService, INotificationCommand notificationCommand,
             IUsuarioContext httpContextAccessor, IMessageApplication messageApplication, IColaboradorService colaboradorService, ICEPService cepService) : base(clubService, alunoService, userManager, notificationCommand, httpContextAccessor, messageApplication)
        {
            _logger = logger;
            _alunoService = alunoService;
            _clubService = clubService;
            _colaboradorService = colaboradorService;
            _cepService = cepService;
        
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            if (User != null)
            {
                await base.RefreshImageUser(User);
                await base.RefreshNotifications(User);
            }


            var homeViewModel = new HomeViewModel();

            var patrocinadores = await _colaboradorService.GetAllAtivos();
            patrocinadores = patrocinadores.Where(x => x.OrdemApresentacao > 0);

            homeViewModel.InformarPatrocinadores(patrocinadores.Skip(0).Take(5));


            return View(homeViewModel);
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
            Exception exception;

            if (model.PreenchidoCorretamente())
            {

              


                if (await base.Autenticar(model))
                    return RedirectToAction("Index");
                else
                {
                    exception = new Exception("Usuário ou senha inválido(s), por favor verifique.");
                    return Json(exception);
                }

            }
            else
            {
                exception = new Exception("Preencha os campos corretamente");
                return Json(exception);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            if (HttpContext != null)
            {
                await HttpContext?.SignOutAsync("cookies");
                _usuarioContext.Clear();
            }


            return View("Login");
        }


        public IActionResult Privacy()
        {

            return View();
        }

        public async Task<IActionResult> About()
        {
            await base.RefreshImageUser(User);
            await base.RefreshNotifications(User);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SobreNos()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




        [HttpGet]
        public async Task<JsonResult> ObterDadosCEP(string CEP)
        {
            var result = await _cepService.GetEnderecoByCEPAsync(CEP);
            return Json(result);
        }
    }
}