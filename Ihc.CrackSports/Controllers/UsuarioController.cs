using Ihc.CrackSports.Core.Requests;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.WebApp.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace Ihc.CrackSports.WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        protected readonly IUsuarioService _usuarioService;


        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }



        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Cadastro(string nome, string cpfCnpj, string email)
        {
            var obj = new UsuarioViewModel() { Nome = nome, Cpf = cpfCnpj, Email = email };
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro([FromBody] CadastroRequest model)
        {
            var result = await _usuarioService.InsertOrUpdate(model);
            return Json(result);
        }
    }
}
