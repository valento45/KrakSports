using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Claims;
using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Notifications.Hubs;
using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Objetos.Competicoes;
using Ihc.CrackSports.Core.Security;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.Core.Utils.Paginacoes;
using Ihc.CrackSports.WebApp.Application.Interfaces;
using Ihc.CrackSports.WebApp.Models.Clube;
using Ihc.CrackSports.WebApp.Models.MessagesViewModel.Information;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ihc.CrackSports.WebApp.Controllers
{
    public class ClubController : ControllerBase
    {
        private readonly IClubService _clubService;

        private readonly IUsuarioService _usuarioService;
        private readonly IAlunoService _alunoService;
        private readonly IClubApplication _clubApplication;
        private bool _isInsert;

        public ClubController(IClubService clubService, UserManager<Usuario> user, IUsuarioService usuarioService, IAlunoService alunoService, IClubApplication clubApplication, INotificationCommand notificationCommand, IUsuarioContext httpContextAccessor, IMessageApplication messageApplication)
            : base(clubService, alunoService, user, notificationCommand, httpContextAccessor, messageApplication)
        {
            _clubService = clubService;
            _usuarioService = usuarioService;
            _alunoService = alunoService;
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
        public async Task<IActionResult> Cadastro(long idUsuario)
        {
            ClubViewModel model = new ClubViewModel();
            model.DadosClub = await _clubService.ObterByIdUsuario(idUsuario) ?? throw new ArgumentNullException("Usuário inválido !");


            if (User.IsAuthenticated())
            {
                if (model.DadosClub.IdUsuario!= long.Parse(User.GetIdentificador()))
                    if (!base.CanRead(User, Roles.CLUB))
                        return View("Unauthorized");
            }
            else
                return View("Unauthorized");


            if (idUsuario <= 0)
                model.Message = "Usuário inválido !";

            await base.RefreshImageUser(User);
            await base.RefreshNotifications(User);



            
            model.DadosUsuario = await _usuarioService.GetById(idUsuario);

            return View("Partial/Club/CadastroClubPartial", model);
        }


        [HttpGet]
        public async Task<IActionResult> CadastroClubSemUsuario(long idClub)
        {

            ClubViewModel model = new ClubViewModel();
            if (idClub <= 0)
                model.Message = "Clube inválido !";

            if(!User.IsAuthenticated() || !User.IsAdm())
                return View("Unauthorized");

            model.DadosClub = await _clubService.ObterById(idClub) ?? throw new ArgumentNullException("Clube inválido !");        


            await base.RefreshImageUser(User);
            await base.RefreshNotifications(User);           

            if (model.DadosClub?.IdUsuario > 0 )
                model.DadosUsuario = await _usuarioService?.GetById(model.DadosClub.IdUsuario);
            else
                model.DadosUsuario = new Usuario();

            return View("Partial/Club/CadastroClubPartial", model);
        }


        [HttpPost]
        public async Task<IActionResult> Cadastro(ClubViewModel model)
        {
            if (model != null)
            {
                _isInsert = model.isInsert();
                if (!_isInsert)
                {
                    if (model.DadosUsuario.Id != long.Parse(User.GetIdentificador()))
                        if (!base.CanUpdate(User, Roles.CLUB))
                            return View("Unauthorized");
                }

                if (model.DadosUsuario?.Id <= 0)
                {
                    var user = await _userManager.FindByIdAsync(model.DadosUsuario.UserName);

                    if (user == null)
                    {
                        user = new Usuario()
                        {
                            UserName = model.DadosUsuario.UserName,
                            PasswordHash = model.DadosUsuario.PasswordHash,
                            Email = model.DadosUsuario.Email,
                            NormalizedUserName = model.DadosUsuario.UserName.ToUpper(),
                            TipoUsuario = Core.Objetos.Enums.TipoUsuario.Club
                        };

                        var resultUser = await _userManager.CreateAsync(user, user.PasswordHash);

                        if (resultUser.Succeeded)
                        {
                            user = await _userManager.FindByNameAsync(user.UserName);
                            await _userManager.AddClaimsAsync(user, new List<Claim> { new Claim(ClaimTypes.Role, Roles.CLUB) });
                            model.DadosClub.IdUsuario = user.Id;
                        }
                    }
                    else if (_isInsert)
                    {
                        model.Message = "Usuário informado já existe !";
                        return View("Partial/Club/CadastroClubPartial", model);
                    }
                }
                else
                {
                    var user = await _userManager.FindByIdAsync(model.DadosClub.IdUsuario.ToString());



                    if (user == null)
                    {
                        return NotFound("Usuário não encontrado.");
                    }

                    // Gerar token para resetar a senha
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    // Resetar a senha
                    var resultado = await _userManager.ResetPasswordAsync(user, token, model.DadosUsuario.PasswordHash);

                    if (resultado.Succeeded)
                    {
                        await _userManager.UpdateAsync(user);                      
                    }
                }

                if (model.File != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        await model.File.CopyToAsync(ms);
                        model.DadosClub.ImagemBase64 = Convert.ToBase64String(ms.ToArray());
                        HttpContext.Session.SetString("photo", model.DadosClub.ImagemBase64);

                    }
                }

                var result = await _clubService.Salvar(model.DadosClub);

                if (result.IsSuccessStatusCode)
                {
                    if (_isInsert)
                        await _notificationCommand.NotificarAdministradoresClubeCadastrado(model.DadosClub);


                    var message = await _messageApplication.GetMessage(model.DadosClub, result.IsInsert ? TipoMessage.Insercao : TipoMessage.Alteracao);
                    return View("Partial/MessagesInformation/_MessageInformation", message);
                }
                else
                    model.Message = $"Erro ao salvar dados do club. Codigo {result.StatusCode} - {result.Message}";

            }

            return View("Partial/Club/CadastroClubPartial", model);
        }


        [HttpGet]
        public async Task<IActionResult> AtualizarCamisa(int idAluno)
        {
            await base.RefreshNotifications(User);
            var aluno = await _alunoService.GetById(idAluno);

            return View("Partial/Club/_ModalAlterarCamisa", aluno);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarCamisa(Aluno aluno)
        {

            var result = await _alunoService.AtualizarCamisa(aluno);
            if (result)
                return RedirectToAction("MinhaConta", "Usuario");
            else
                throw new Exception("Falha ao atualizar o número da camiseta.");
        }

        [HttpGet]
        public async Task<IActionResult> VerClubs(int limite)
        {
            await base.RefreshNotifications(User);

            var clubes = await _clubService.ObterTodos(limite, true);
            var result = new PaginacaoClubViewModel(new Paginacao<Club>(clubes?.AsQueryable(), 1, 10));

            ViewBag.paginacao = result;

            return View(result);
        }


        [HttpPost]
        public async Task<PartialViewResult> RefreshPaginacaoClub([FromBody] PaginacaoClubViewModelRequest paginacaoClubs)
        {
            var result = new PaginacaoClubViewModel(paginacaoClubs);
            await result.Refresh();

            ViewBag.paginacao = result;
            return PartialView("Partial/_PartialClubs", result);
        }


        [HttpGet]
        public async Task<IActionResult> ApresentacaoClub(long idClub)
        {
            await base.RefreshNotifications(User);
            ClubViewModel result = await _clubApplication.GetClubViewModelByIdClube(idClub);
            result.Atletas.CanUpdate = false;

            if (result.DadosClub?.IdUsuario > 0)
                result.DadosUsuario = await _usuarioService.GetById(result.DadosClub.IdUsuario);

            return View(result);
        }



        [HttpGet]
        public async Task<PartialViewResult> SelecionarClubePartial()
        {
            var result = await _clubService.ObterTodos(0, true);
            return PartialView("Partial/Club/_ModalSelecionarClube", result);
        }


        [HttpGet]
        public async Task<IActionResult> VerSolicitacaoClube(long idClub)
        {
            return View("_DetalhesClubeAdminPartial");
        }



    }
}
