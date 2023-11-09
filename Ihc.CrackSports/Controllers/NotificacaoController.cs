using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Claims;
using Ihc.CrackSports.Core.Authorization.Context;
using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Commands;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Objetos.Notifications.SolicitacoesPedidos;
using Ihc.CrackSports.Core.Requests.Notifications;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.WebApp.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Ihc.CrackSports.WebApp.Controllers
{
    public class NotificacaoController : ControllerBase
    {
        public NotificacaoController(IClubService clubService, IAlunoService alunoService, UserManager<Usuario> userManager, INotificationCommand notificationCommand, IUsuarioContext httpContextAccessor, IMessageApplication messageApplication)
            : base(clubService, alunoService, userManager, notificationCommand, httpContextAccessor, messageApplication)
        {

        }


        [HttpGet]
        public async Task<PartialViewResult> RefreshNotificationsPartialView()
        {
            var result = await _usuarioContext.GetNotificacoes();

            if (User.IsAuthenticated())
            {
                var request = new NotificationRequest(long.Parse(User.GetIdentificador()), User.GetTipoUsuario());
                var notifications = await _notificationCommand.ObterTodasNotificacoes(request);

                result = notifications.ToList();
            }

            return PartialView("Partial/Notificacoes/_NotificacoesPartial", result);
        }


        [HttpPost]
        public async Task<JsonResult> ExcluirNotificacao(long idNotificacao)
        {
            var result = await _notificationCommand.RemoverSolicitacao(idNotificacao);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> AceitarSolicitacaoAluno([FromBody] long idNotificacao)
        {
            var solicitacao = await _notificationCommand.ObterSolicitacaoAlunoById(idNotificacao);

            if (await _notificationCommand.AceitarSolicitacao(solicitacao)) 
                return Json(solicitacao);
            else
                throw new Exception("Não foi possível aceitar a solicitação no momento.");
        }


        [HttpPost]
        public async Task<IActionResult> EnviaSolicitacaoClub([FromBody] UserAlunoToClube request)
        {

            var aluno = await _alunoService.GetByIdUsuario(request.IdUsuario);

            await _notificationCommand.TrataEnvioSolicitacaoAlunoToClub(aluno.Id, request.IdClub);

            return Ok();
        }
    }
}
