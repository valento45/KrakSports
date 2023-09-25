using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Context;
using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Ihc.CrackSports.WebApp.Controllers
{
    public class NotificacaoController : ControllerBase
    {
        public NotificacaoController(IClubService clubService, IAlunoService alunoService, UserManager<Usuario> userManager, INotificationCommand notificationCommand, IUsuarioContext httpContextAccessor) : base(clubService, alunoService, userManager, notificationCommand, httpContextAccessor)
        {

        }


        [HttpGet]
        public async Task<PartialViewResult> RefreshNotificationsPartialView()
        {
            var result = await _usuarioContext.GetNotificacoes();
            return PartialView("Partial/Notificacoes/_NotificacoesPartial", result);
        }
    }
}
