using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ihc.CrackSports.WebApp.Controllers
{
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventoController(IClubService clubService, IAlunoService alunoService, UserManager<Usuario> userManager, INotificationCommand notificationCommand,
            IUsuarioContext httpContextAccessor, IEventoService eventoService) : base(clubService, alunoService, userManager, notificationCommand, httpContextAccessor)
        {
            _eventoService = eventoService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var de = DateTime.Parse($"01/{DateTime.Now.Month}/{DateTime.Now.Year}");
            var ate = DateTime.Now.AddMonths(1).AddDays(-1);

            var result = await _eventoService.GetEventos(de, ate);

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> CadastroEvento()
        {
            return View();
        }
    }
}
