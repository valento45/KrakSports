using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Claims;
using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Notifications.Hubs;
using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Objetos.Enums;
using Ihc.CrackSports.Core.Requests.Notifications;
using Ihc.CrackSports.Core.Security;
using Ihc.CrackSports.Core.Services;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.WebApp.Application.Interfaces;
using Ihc.CrackSports.WebApp.Models.Usuarios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Data;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using static Dapper.SqlMapper;

namespace Ihc.CrackSports.WebApp.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly INotificationCommand _notificationCommand;
        protected readonly IUsuarioContext _usuarioContext;
        protected readonly IMessageApplication _messageApplication;
        protected IAlunoService _alunoService;
        protected IClubService _clubService;
        protected readonly UserManager<Usuario> _userManager;
        protected readonly NotificationHub _notificationHub;



        #region Construtores        

        public ControllerBase(IClubService clubService, IAlunoService alunoService, UserManager<Usuario> userManager, INotificationCommand notificationCommand, IUsuarioContext httpContextAccessor, IMessageApplication messageApplication)
        {
            _clubService = clubService;
            _alunoService = alunoService;
            _userManager = userManager;
            _notificationCommand = notificationCommand;
            _usuarioContext = httpContextAccessor;
            _messageApplication = messageApplication;
        }

        #endregion


        protected async Task RefreshImageUser(ClaimsPrincipal User)
        {
            if (User != null)
            {
                if (!HttpContext.Session.Keys.Any(x => x == "photo"))
                {
                    if (User.IsAuthenticated())
                    {
                        if (User.IsAluno())
                        {
                            var aluno = await _alunoService.GetByIdUsuario(long.Parse(User.GetIdentificador()));

                            if (aluno != null)
                                _usuarioContext.SetImage(aluno.FotoAlunoBase64);

                        }
                        else if (User.IsClub())
                        {
                            var club = await _clubService.ObterByIdUsuario(long.Parse(User.GetIdentificador()));
                            if (club != null)
                                _usuarioContext.SetImage(club.ImagemBase64);
                        }
                    }
                }
            }
        }

        protected async Task RefreshNotifications(ClaimsPrincipal User)
        {
            if (User != null)
            {
                if (User.IsAuthenticated())
                {
                    if (!Roles.Notificacoes?.Any() ?? true)
                    {
                        var notificacaoRequest = new NotificationRequest(GetIdUsuarioLogado(), User.GetTipoUsuario());

                        Roles.Notificacoes = await _notificationCommand.ObterTodasNotificacoes(notificacaoRequest);
                    }
                }
            }
        }

        protected bool CanAccess(ClaimsPrincipal User, string role)
        {
            return User?.CanAccess(role) ?? false;
        }

        protected bool CanInsert(ClaimsPrincipal User, string role)
        {
            return User?.CanInsert(role) ?? false;
        }

        protected bool CanUpdate(ClaimsPrincipal User, string role)
        {
            return User?.CanUpdate(role) ?? false;
        }

        protected bool CanDelete(ClaimsPrincipal User, string role)
        {
            return User?.CanDelete(role) ?? false;
        }

        protected bool CanRead(ClaimsPrincipal User, string role)
        {
            return User?.CanRead(role) ?? false;
        }
        protected long GetIdUsuarioLogado()
        {
            var claimUser = HttpContext?.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier);

            if (claimUser != null)
            {
                return long.Parse(claimUser.Value.ToString());
            }
            else
                return -1;
        }


        protected string GetUserNameLogado()
        {
            var claimUser = HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name);

            if (claimUser != null)
            {
                return claimUser.Value.ToString();
            }
            else
                return "";
        }


        protected string GetPasswordHashLogado()
        {
            var claimUser = HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Hash);

            if (claimUser != null)
            {
                return claimUser.Value.ToString();
            }
            else
                return "";
        }

        protected bool IsAluno()
        {
            return User?.IsAluno() ?? false;
        }

        protected async Task ConfiguraUserAluno(Usuario user, IUsuarioContext httpContextAccessor)
        {
            var aluno = await _alunoService.GetByIdUsuario(user.Id);

            if (aluno != null && !string.IsNullOrEmpty(aluno.FotoAlunoBase64))
            {
                httpContextAccessor?.SetImage(aluno.FotoAlunoBase64);


                var notificacoes = await _notificationCommand.ObterTodasNotificacoes(new NotificationRequest(user.Id, (int)TipoUsuario.Aluno));
                httpContextAccessor?.SetNotificacoes(notificacoes.ToList());
            }
        }


        protected async Task ConfiguraUserClub(Usuario user, IUsuarioContext httpContextAccessor)
        {
            var club = await _clubService.ObterByIdUsuario(user.Id);

            if (club != null && !string.IsNullOrEmpty(club.ImagemBase64))
            {
                httpContextAccessor.SetImage(club.ImagemBase64);

                var notificacoes = await _notificationCommand.ObterTodasNotificacoes(new NotificationRequest(user.Id, (int)TipoUsuario.Club));
                httpContextAccessor?.SetNotificacoes(notificacoes.ToList());
            }
        }

        protected async Task<bool> Autenticar(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, Security.Encrypt(model.PasswordHash)))
                {
                    var identity = new ClaimsIdentity("cookies");

                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Name, model.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.Hash, user.PasswordHash));
                    identity.AddClaims(await _userManager.GetClaimsAsync(user));


                    if (identity.HasClaim(x => x.Value == Roles.ALUNO))
                        await ConfiguraUserAluno(user, _usuarioContext);

                    else if (identity.HasClaim(x => x.Value == Roles.CLUB))
                        await ConfiguraUserClub(user, _usuarioContext);


                    var userClaim = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync("cookies", userClaim);


                    return true;
                }
            }

            return false;
        }

        public async Task EnviarNotificacao(string user, string title, string message, string link)
        {
            //await _notificationHub.SendNotification(user, title, message, link);
        }
    }
}
