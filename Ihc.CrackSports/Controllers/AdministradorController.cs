using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Claims;
using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Objetos.Colaborador;
using Ihc.CrackSports.Core.Objetos.Competicoes;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.Core.Utils.Paginacoes;
using Ihc.CrackSports.Core.ViewModel.Admin.ClubeAdm;
using Ihc.CrackSports.Core.ViewModel.Base;
using Ihc.CrackSports.Core.ViewModel.Colaborador;
using Ihc.CrackSports.WebApp.Application.Interfaces;
using Ihc.CrackSports.WebApp.Models.MessagesViewModel.Information;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.WebApp.Models.Alunos;

namespace Ihc.CrackSports.WebApp.Controllers
{
    public class AdministradorController : ControllerBase
    {
        private readonly IColaboradorService _colaboradorService;
        private readonly IUsuarioService _usuarioService;

        public AdministradorController(IClubService clubService, IAlunoService alunoService, UserManager<Usuario> userManager, INotificationCommand notificationCommand,
            IUsuarioContext httpContextAccessor, IMessageApplication messageApplication, IColaboradorService colaboradorService, IUsuarioService usuarioService)
            : base(clubService, alunoService, userManager, notificationCommand, httpContextAccessor, messageApplication)
        {
            _colaboradorService = colaboradorService;
            _usuarioService = usuarioService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if ((User?.IsAuthenticated() ?? false) && User.IsAdm())
            {
                var usuarioLogado = await _userManager.FindByIdAsync(User.GetIdentificador());
                return View(usuarioLogado);
            }
            else
                return View("Unauthorized");

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Home()
        {
            if ((User?.IsAuthenticated() ?? false) && User.IsAdm())
            {
                var usuarioLogado = await _userManager.FindByIdAsync(User.GetIdentificador());
                return View();
            }
            else
                return View("Unauthorized");

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Patrocinadores()
        {
            if ((User?.IsAuthenticated() ?? false) && User.IsAdm())
            {
                var usuarioLogado = await _userManager.FindByIdAsync(User.GetIdentificador());
                if (!usuarioLogado.Claims?.Any(x => x.Value == Roles.ADMINISTRADOR) ?? true)
                {
                    return View("Unauthorized");
                }

                PatrocinadoresAdminViewModel result = new PatrocinadoresAdminViewModel() { PageSize = 6 };

                result.InformarAtivos(await result.InicializarPaginacao(await _colaboradorService.GetAllAtivos()));
                result.InformarSolicitacoes(await result.InicializarPaginacao(await _colaboradorService.GetAllPendentes()));
                result.InformarInativos(await result.InicializarPaginacao(await _colaboradorService.GetAllInativos()));

                return View(result);
            }
            else
                return View("Unauthorized");

        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DetalhesPatrocinadorPartialView([FromBody] Patrocinador patrocinador)
        {
            return View("Partial/_DetalhesPatrocinadorAdminPartial", patrocinador);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AtualizarDadosPatrocinador(Patrocinador patrocinador)
        {

            if ((User?.IsAuthenticated() ?? false) && User.IsAdm())
            {



                var result = await _colaboradorService.AtualizarPatrocinador(patrocinador);

                if (result)
                {
                    var msgResponse = new MessageInformationViewModel(TipoMessage.Alteracao);
                    msgResponse.InformarMessage("O cadastro do patrocinador foi atualizado com sucesso.");

                    return View("Partial/MessagesInformation/_MessageInformation", msgResponse);
                }
                else
                    throw new Exception("Erro ao salvar dados do patrocinador");
            }
            else
                return View("Unauthorized");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ClubesAdmin()
        {
            var todosClubes = await _clubService.ObterTodos();

            if (todosClubes?.Any() ?? false)
            {
                var paginacao = new Paginacao<Club>(todosClubes.AsQueryable(), 1, 10);
                var result = new PaginacaoBaseViewModel<Club>(paginacao);

                await result.Refresh();


                ClubePaginacaoAdminViewModel clubePaginacaoAdminViewModel = new ClubePaginacaoAdminViewModel(result);

                return View(clubePaginacaoAdminViewModel);
            }

            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AtletasAdministrativo()
        {
            var todosAlunos = await _alunoService.ObterTodosAluno();

            if (todosAlunos?.Any() ?? false)
            {
                PaginacaoAlunoViewModel paginacaoControl = new PaginacaoAlunoViewModel(todosAlunos);
                return View(paginacaoControl);
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<JsonResult> AceitarClube([FromBody] long idClube)
        {

            var clube = await _clubService.ObterById(idClube);

            if (clube != null)
            {
                clube.IsVerificado = true;

                return Json(await _clubService.Salvar(clube));
            }
            return Json(false);
        }

        [Authorize]
        [HttpPost]
        public async Task<JsonResult> RemoverClube([FromBody] long idClube)
        {
            var clube = await _clubService.ObterById(idClube);

            if (clube != null)
            {
                await _notificationCommand.ExcluirNotificacoesClube(idClube);
                var result = await _clubService.Excluir(idClube);

                if (result?.IsSuccessStatusCode ?? false)
                {
                    await _usuarioService.Excluir(clube.IdUsuario);

                    return Json(result);
                }
            }

            return Json(new Exception("Não foi possível excluir o Clube! Por favor, tente mais tarde."));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DetalhesClubePartialView([FromBody] Club club)
        {

            return View("Partial/_DetalhesClubeAdminPartial", club);
        }




        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ExcluirAtleta(int idAtleta)
        {

            if (await base._alunoService.ExcluirAluno(idAtleta))
                return View("SucessoMessage");

            else
                return NoContent();
        }



        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AtualizarDadosUsuarioAluno(int idAluno)
        {


            var aluno = await base._alunoService.GetById(idAluno);


            var usuario = await  _usuarioService.GetById(aluno.IdUsuario);



            return View("AtualizarDadosUsuario", usuario);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SalvarDadosUsuario(Usuario usuario)
        {

            var user = await base._userManager.FindByIdAsync(usuario.Id.ToString());

            if(user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Gerar token para resetar a senha
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Resetar a senha
            var resultado = await _userManager.ResetPasswordAsync(user, token, usuario.PasswordHash);

            if (resultado.Succeeded)
            {
                await _userManager.UpdateAsync(user);
                return View("SucessoMessage");
            }
            else
                return BadRequest();

            //var result = await _usuarioService.AtualizarUsuario(usuario);

            //if (result)
            //    return View("SucessoMessage");
            //else
            //    return BadRequest();
        }

    }
}
