using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Colaborador;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.Core.Utils.Paginacoes;
using Ihc.CrackSports.Core.ViewModel.Colaborador;
using Ihc.CrackSports.WebApp.Application;
using Ihc.CrackSports.WebApp.Application.Interfaces;
using Ihc.CrackSports.WebApp.Models.MessagesViewModel.Information;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ihc.CrackSports.WebApp.Controllers
{
    public class ColaboradorController : ControllerBase
    {
        private readonly IColaboradorService _colaboradorService;

        public ColaboradorController(IClubService clubService, IAlunoService alunoService, UserManager<Usuario> userManager, INotificationCommand notificationCommand, IUsuarioContext httpContextAccessor, IMessageApplication messageApplication,
            IColaboradorService colaboradorService)
            : base(clubService, alunoService, userManager, notificationCommand, httpContextAccessor, messageApplication)
        {
            _colaboradorService = colaboradorService;
        }

        [HttpGet]
        public IActionResult SejaUmColaborador()
        {
            return View();
        }


        [HttpGet]
        public IActionResult CadastroPatrocinador()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NovoPatrocinador([FromBody] Patrocinador patrocinador)
        {
            var insertPatrocinador = await _colaboradorService.NovoPatrocinador(patrocinador);

            if (!insertPatrocinador)
                return View("Partial/MessagesInformation/_MessageInformation", new MessageInformationViewModel(TipoMessage.Erro));



            var result = await _messageApplication.GetMessage(patrocinador, TipoMessage.Insercao, insertPatrocinador);

            return View("Partial/MessagesInformation/_MessageInformation", result);
        }

        [HttpPost]
        public async Task<PartialViewResult> RefreshPaginacaoColaborador([FromBody] PaginacaoPatrocinadorViewModelRequest request)
        {

            PaginacaoPatrocinadorViewModel result = new PaginacaoPatrocinadorViewModel(request);

            await result.Refresh();

            return PartialView("Partial/Patrocinadores/_ListaPatrocinadoresPartial", result);
        }





        [HttpPost]
        public async Task<JsonResult> AceitarPatrocinador([FromBody] Patrocinador model)
        {
            model.Status = StatusPatrocinador.Ativo;
            var result = await _colaboradorService.AtualizarPatrocinador(model);

            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> ExcluirPatrocinador([FromBody] Patrocinador model)
        {
            var result = await _colaboradorService.ExcluirPatrocinador(model.IdPatrocinador);

            return Json(result);
        }


        [HttpPost]
        public async Task<JsonResult> InativarPatrocinador([FromBody] Patrocinador model)
        {
            model.Status = StatusPatrocinador.Bloqueado;
            var result = await _colaboradorService.AtualizarPatrocinador(model);

            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> ReativarPatrocinador([FromBody] Patrocinador model)
        {
            model.Status = StatusPatrocinador.Ativo;
            var result = await _colaboradorService.AtualizarPatrocinador(model);

            return Json(result);
        }


        [HttpGet]
        public async Task<IActionResult> ConfirmarInativacaoPatrocinador(long idPatrocinador)
        {
            var obj = await _colaboradorService.GetAllAtivos();

            var result = obj.FirstOrDefault(x => x.IdPatrocinador == idPatrocinador);

            return View("Partial/Administrador/_ModalConfirmInaPatrocinador", result);
        }


        [HttpGet]
        public async Task<IActionResult> VerTodosPatrocinadores()
        {

            var patrocinadores = await _colaboradorService.GetAllAtivos();
            var paginacaoPatrocinadores = new Paginacao<Patrocinador>(patrocinadores.AsQueryable(), 1, 10);


            PaginacaoPatrocinadorViewModel result = new PaginacaoPatrocinadorViewModel(paginacaoPatrocinadores);
            await result.Refresh();


            return View(result);
        }


        [HttpPost]
        public async Task<PartialViewResult> RefreshPaginacaoVerTodosPatrocinadores([FromBody] PaginacaoPatrocinadorViewModelRequest request)
        {
            request.PageSize = 10;
            PaginacaoPatrocinadorViewModel result = new PaginacaoPatrocinadorViewModel(request);

            await result.Refresh();

            return PartialView("Partial/Patrocinadores/PaginacaoVerTodosPatrocinadores", result);
        }
    }
}
