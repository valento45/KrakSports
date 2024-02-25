using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Claims;
using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.AgendaEventos;
using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Competicoes;
using Ihc.CrackSports.Core.Requests.Agenda;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.WebApp.Application.Interfaces;
using Ihc.CrackSports.WebApp.Models.EventoModels;
using Ihc.CrackSports.WebApp.Models.Eventos;
using Ihc.CrackSports.WebApp.Models.MessagesViewModel.Information;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ihc.CrackSports.WebApp.Controllers
{
    public class EventoController : ControllerBase
    {
        private readonly IEventoApplication _eventoApplication;

        public EventoController(IClubService clubService, IAlunoService alunoService, UserManager<Usuario> userManager, INotificationCommand notificationCommand,
            IUsuarioContext httpContextAccessor, IEventoApplication eventoService, IMessageApplication messageApplication) : base(clubService, alunoService, userManager, notificationCommand, httpContextAccessor, messageApplication)
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
        public async Task<IActionResult> CadastroEvento()
        {

            if ((User?.IsAuthenticated() ?? false) && (User.IsClub() || User.IsAdm()))
            {
                var evento = new Evento();
                return View(evento);
            }
            else
                return View("Unauthorized");

        }

        [HttpPost]
        public async Task<IActionResult> SalvarEvento(Evento evento)
        {

            if (await _eventoApplication.Salvar(evento))
            {
                var result = await _messageApplication.GetMessage(evento, TipoMessage.Insercao);

                return View("Partial/MessagesInformation/_MessageInformation", result);
            }

            return View("Partial/MessagesInformation/_MessageInformation", await _messageApplication.GetMessage(evento, TipoMessage.Erro));
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

        [HttpGet]
        public async Task<IActionResult> LancarResultado(long idEvento)
        {
            var evento = await _eventoApplication.GetEventoById(idEvento);

            evento.Clube1.InformarAtletas(await _alunoService.ObterAlunosPorClub(evento.Clube1.Id));
            evento.Clube2.InformarAtletas(await _alunoService.ObterAlunosPorClub(evento.Clube2.Id));

            var placarAtlteas = await _eventoApplication.ObterPlacar(idEvento);

            evento.InformarAtletasClubeUm(placarAtlteas.Where(x => x.IdClube == evento.Clube1.Id).ToList());
            evento.InformarAtletasClubeDois(placarAtlteas.Where(x => x.IdClube == evento.Clube2.Id).ToList());

            return View(evento);
        }

        [HttpPost]
        public async Task<JsonResult> AdicionarGol([FromBody] AtletaEventoGolsViewModel model)
        {
            var obj = new AtletaEvento(model.Aluno, model.Evento.IdEvento, model.Gols);

            var result = await _eventoApplication.LancarPlacarEvento(obj);

            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> RemoverGol([FromBody] long idLancamento)
        {
            var result = await _eventoApplication.ExcluirLancamentoPlacar(idLancamento);

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> VerEvento(long idEvento)
        {
            var evento = await _eventoApplication.GetEventoById(idEvento);

            return View(evento);
        }


        [HttpGet]
        public async Task<IActionResult> EscalarEquipe(long idEvento)
        {
            var idUser = base.GetIdUsuarioLogado();
            var clube = await _clubService.ObterByIdUsuario(idUser);

            if (clube == null)
                return Unauthorized();


            clube.InformarAtletas(await _alunoService.ObterAlunosPorClub(clube.Id));

            var viewModel = new EscalarEquipeEventoViewModel();
            viewModel.InformarClube(clube);
            viewModel.InformarEvento(await _eventoApplication.GetEventoById(idEvento));
            viewModel.InformarJogadoresEscalados(new List<AtletaEvento>());

            return View(viewModel);
        }
    }
}
