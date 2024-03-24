using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Claims;
using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Notifications.Hubs;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Objetos.Enums;
using Ihc.CrackSports.Core.Requests.Aluno;
using Ihc.CrackSports.Core.Security;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.WebApp.Application.Interfaces;
using Ihc.CrackSports.WebApp.Models.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ihc.CrackSports.WebApp.Controllers
{
    public class UsuarioController : ControllerBase
    {
        protected readonly IUsuarioService _usuarioService;


        private readonly IAlunoApplication _alunoApplication;
        private readonly IClubApplication _clubApplication;

        public UsuarioController(IUsuarioService usuarioService, UserManager<Usuario> user, IAlunoService alunoService, IClubService clubService,
            IAlunoApplication alunoApplication, IClubApplication clubApplication, INotificationCommand notificationCommand, IUsuarioContext httpContextAccessor, IMessageApplication messageApplication) : base(clubService, alunoService, user, notificationCommand, httpContextAccessor, messageApplication)
        {
            _usuarioService = usuarioService;
            _alunoApplication = alunoApplication;
            _clubApplication = clubApplication;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await base.RefreshImageUser(User);
            await base.RefreshNotifications(User);

            return View();
        }


        [HttpGet]
        public IActionResult Cadastro(string nome, string cpfCnpj, string email, TipoUsuario tipoUsuario)
        {
            var obj = new UsuarioViewModel() { Nome = nome, Cpf = cpfCnpj, Email = email, Tipo = tipoUsuario };
            return View(obj);
        }

        /// <summary>
        /// Efetua o cadastro de Aluno
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<IActionResult> Cadastro([FromBody] CadastroRequest model)
        {

            var user = await _userManager.FindByNameAsync(model.Login);

            if (user == null)
            {
                user = new Usuario()
                {
                    UserName = model.Login,
                    PasswordHash = model.Senha,
                    Email = model.Email,
                    NormalizedUserName = model.Login.ToUpper(),
                    TipoUsuario = TipoUsuario.Aluno
                };

                var result = await _userManager.CreateAsync(user, Security.Encrypt(user.PasswordHash));

                if (result.Succeeded)
                {
                    user = await _userManager.FindByNameAsync(model.Login);
                    await _userManager.AddClaimsAsync(user, new List<Claim> { new Claim(ClaimTypes.Role, Roles.ALUNO) });
                    await _alunoService.InsertOrUpdate(model, user.Id);

                    return View("SuccessCadastro", user);
                }
                else
                    return Json(new Exception("Não foi possível cadastrar o usuário! Por favor, tente novamente mais tarde."));

            }
            else
                return Json(new Exception("O usuário informado já existe"));

        }


        /// <summary>
        /// Acesso ao Minha conta para o usuario logado
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="tipoUsuario"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> MinhaConta(long idUsuario, TipoUsuario tipoUsuario)
        {
            await base.RefreshImageUser(User);
            await base.RefreshNotifications(User);

            if (User.IsAuthenticated())
            {
                long id;
                MinhaContaViewModel viewModel = new MinhaContaViewModel();
                if (long.TryParse(User.GetIdentificador(), out id))
                {


                    if (idUsuario > 0 && (idUsuario == id || CanAccess(User, Roles.ADMINISTRADOR)))
                    {


                        if (tipoUsuario == TipoUsuario.Aluno || User.IsAdm())
                            viewModel = await _alunoApplication.GetAlunoViewModel(idUsuario);

                        else if (tipoUsuario == TipoUsuario.Club || User.IsAdm())
                        {
                            viewModel.TipoUsuario = TipoUsuario.Club;
                            viewModel.ClubViewModel = await _clubApplication.GetClubViewModel(idUsuario);
                        }
                        return View(viewModel);
                    }
                    else if (idUsuario <= 0)
                    {
                        if (User.IsAluno())
                        {
                            viewModel = await _alunoApplication.GetAlunoViewModel(id);
                        }
                        else if (User.IsClub())
                        {
                            viewModel.TipoUsuario = TipoUsuario.Club;
                            viewModel.ClubViewModel = await _clubApplication.GetClubViewModel(id);
                        }
                        return View(viewModel);
                    }
                    else
                    {
                        return View("Unauthorized");
                    }
                }
            }
            return View("Unauthorized");
        }
    }
}
