using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;

namespace Ihc.CrackSports.WebApp.Controllers
{
    public class NotificacaoController : ControllerBase
    {
        public NotificacaoController(IClubService clubService, IAlunoService alunoService, UserManager<Usuario> userManager, INotificationCommand notificationCommand, IUsuarioContext httpContextAccessor) : base(clubService, alunoService, userManager, notificationCommand, httpContextAccessor)
        {

        }

        
    }
}
