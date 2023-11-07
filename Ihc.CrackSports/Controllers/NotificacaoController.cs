using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Claims;
using Ihc.CrackSports.Core.Authorization.Context;
using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Commands;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Clube;
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
        public async Task<JsonResult> AceitarSolicitacaoAluno(long idNotificacao)
        {
            var solicitacao = await _notificationCommand.ObterSolicitacaoAlunoById(idNotificacao);                
            return Json(await _notificationCommand.AceitarSolicitacao(solicitacao));
        }


        [HttpPost]
        public async Task<IActionResult> EnviaSolicitacaoClub(long idUsuario, long idClub)
        {
            var aluno = await _alunoService.GetByIdUsuario(idUsuario);
            
            await _notificationCommand.TrataEnvioSolicitacaoAlunoToClub(aluno.Id, idClub);

            return Ok();
        }
    }
}
