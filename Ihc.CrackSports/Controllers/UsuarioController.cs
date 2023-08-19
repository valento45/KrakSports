using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Requests;
using Ihc.CrackSports.Core.Security;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.WebApp.Models.Usuarios;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ihc.CrackSports.WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        protected readonly IUsuarioService _usuarioService;
        private readonly UserManager<Usuario> _userManager;


        public UsuarioController(IUsuarioService usuarioService, UserManager<Usuario> user)
        {
            _usuarioService = usuarioService;
            _userManager = user;
        }



        [HttpGet]
        public IActionResult Index()
        {
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
                    return View("SuccessCadastro", user);
                }
                else
                    throw new Exception("Ocorreu um erro ao cadastrar o usuário");

            }
            else
                throw new Exception("O usuário informado já existe");

        }
    }
}
