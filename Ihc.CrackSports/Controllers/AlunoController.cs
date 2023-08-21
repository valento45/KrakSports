using Ihc.CrackSports.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ihc.CrackSports.WebApp.Controllers
{
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _alunoService;

        public AlunoController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet]
        public async Task<IActionResult> DadosGerais(long idAluno)
        {
            var obj = await _alunoService.GetById(idAluno);

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> DadosGerais()
        {
            return View();
        }
    }
}
