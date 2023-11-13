using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Claims;
using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Competicoes;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.Core.ViewModel.Colaborador;
using Ihc.CrackSports.WebApp.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ihc.CrackSports.WebApp.Controllers
{
    public class AdministradorController : ControllerBase
    {
        private readonly IColaboradorService _colaboradorService;

        public AdministradorController(IClubService clubService, IAlunoService alunoService, UserManager<Usuario> userManager, INotificationCommand notificationCommand,
            IUsuarioContext httpContextAccessor, IMessageApplication messageApplication, IColaboradorService colaboradorService)
            : base(clubService, alunoService, userManager, notificationCommand, httpContextAccessor, messageApplication)
        {
            _colaboradorService = colaboradorService;
        }

        public async Task<IActionResult> Index()
        {
            if ((User?.IsAuthenticated() ?? false) && User.IsAdm())
            {
                var usuarioLogado = await _userManager.FindByIdAsync(User.GetIdentificador());
                return View(usuarioLogado);
            }
            else
                return View("Unauthorized");

        }

        public async Task<IActionResult> Home()
        {
            if ((User?.IsAuthenticated() ?? false) && User.IsAdm())
            {
                var usuarioLogado = await _userManager.FindByIdAsync(User.GetIdentificador());
                return View();
            }
            else
                return View("Unauthorized");

        }

        [HttpGet]
        public async Task<IActionResult> Patrocinadores()
        {
            if ((User?.IsAuthenticated() ?? false) && User.IsAdm())
            {
                var usuarioLogado = await _userManager.FindByIdAsync(User.GetIdentificador());
                if (!usuarioLogado.Claims?.Any(x => x.Value == Roles.ADMINISTRADOR) ?? true)
                {
                    return View("Unauthorized");
                }

                PatrocinadoresAdminViewModel result = new PatrocinadoresAdminViewModel();
                result.InformarAtivos(await result.InicializarPaginacao(await _colaboradorService.GetAllAtivos()));
                result.InformarSolicitacoes(await result.InicializarPaginacao(await _colaboradorService.GetAllPendentes()));

         

                return View(result);
            }
            else
                return View("Unauthorized");

        }


    }
}
