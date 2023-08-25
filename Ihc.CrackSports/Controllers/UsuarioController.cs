using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Claims;
using Ihc.CrackSports.Core.Requests;
using Ihc.CrackSports.Core.Security;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.WebApp.Models.Usuarios;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ihc.CrackSports.WebApp.Controllers
{
    public class UsuarioController : ControllerBase
    {
        protected readonly IUsuarioService _usuarioService;
        private readonly UserManager<Usuario> _userManager;
        private readonly IAlunoService _alunoService;
        private readonly IClubService _clubService;

        public UsuarioController(IUsuarioService usuarioService, UserManager<Usuario> user, IAlunoService alunoService, IClubService clubService) : base(alunoService)
        {
            _usuarioService = usuarioService;
            _userManager = user;
            _alunoService = alunoService;
            _clubService = clubService;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (User != null)
            {
                await base.RefreshImageUser(User);
            }

            return View();
        }


        [HttpGet]
        public IActionResult Cadastro(string nome, string cpfCnpj, string email)
        {
            var obj = new UsuarioViewModel() { Nome = nome, Cpf = cpfCnpj, Email = email };
            return View(obj);
        }

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
                    NormalizedUserName = model.Login.ToUpper()
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
                    throw new Exception("Ocorreu um erro ao cadastrar o usuário");

            }
            else
                throw new Exception("O usuário informado já existe");

        }

        [HttpGet]
        public async Task<IActionResult> MinhaConta(long idUsuario)
        {
            if (User != null)
            {
                await base.RefreshImageUser(User);
            }

            var claim = HttpContext.User.Claims.FirstOrDefault(param => param.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
                return View("Unauthorized");
            else
            {
                long id;
                if (long.TryParse(claim.Value, out id))
                {
                    if (idUsuario == id || CanAccess(HttpContext.User, Roles.ADMINISTRADOR))
                    {
                        var user = await _usuarioService.GetById(id);
                        var aluno = await _alunoService.GetByIdUsuario(id);
                        

                        var viewModel = new MinhaContaViewModel(aluno, user);
                        viewModel.InformarClub(await _clubService.ObterById(aluno.IdClub) ?? new Core.Objetos.Clube.Club());

                        return View(viewModel);
                    }
                    else
                    {
                        return View("Unauthorized");
                    }
                }
            }

            return View();
        }
    }
}
