using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Claims;
using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Competicoes;
using Ihc.CrackSports.Core.Requests.Agenda;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.WebApp.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ihc.CrackSports.WebApp.Controllers
{
    public class EventoController : ControllerBase
    {
        private readonly IEventoApplication _eventoApplication;

        public EventoController(IClubService clubService, IAlunoService alunoService, UserManager<Usuario> userManager, INotificationCommand notificationCommand,
            IUsuarioContext httpContextAccessor, IEventoApplication eventoService) : base(clubService, alunoService, userManager, notificationCommand, httpContextAccessor)
        {
            _eventoApplication = eventoService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var de = new DateTime();
            var ate = new DateTime();

            var result = await _eventoApplication.GetEventos(de, ate);

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> CadastroEvento(long idEvento)
        {

            if ((User?.IsAuthenticated() ?? false) && (User.IsClub() || User.IsAdm()))
            {
                var evento = idEvento > 0 ? await _eventoApplication.GetEventoById(idEvento) : new Evento();
                return View(evento);
            }
            else
                return View("Unauthorized");

        }


        [HttpPost]
        public async Task<IActionResult> SalvarEvento(Evento evento)
        {

            if (await _eventoApplication.Salvar(evento))
                return View("CadastroEvento", evento);

            throw new ApplicationException("Erro ao salvar o evento. Por favor, tente mais tarde.");
        }

        [HttpPost]
        public async Task<PartialViewResult> ListarEventosPartialView([FromBody] ListarEventosRequest model)
        {
            DateTime de = new DateTime(), ate = new DateTime();

            if (model.IsValido())
            {
                de = DateTime.Parse($"01/{model.Mes}/{model.Ano}");
                ate = de.AddMonths(1).AddDays(-1);
            }


            var result = await _eventoApplication.GetEventos(de, ate);

            return PartialView("Partial/AgendaEventos/_EventosPartial", result);
        }


        [HttpGet]
        public async Task<IActionResult> ConfimarExclusaoEvento(long idEvento)
        {
            var evento = await _eventoApplication.GetEventoById(idEvento);

            return View("Partial/AgendaEventos/Partial/_ModalConfirmExclusaoEvento", evento);
        }


        [HttpPost]
        public async Task<IActionResult> ConfimarExclusaoEvento([FromBody] Evento evento)
        {
            var result = await _eventoApplication.ExcluirEvento(evento.IdEvento);

            return Ok(result);
        }
    }
}
