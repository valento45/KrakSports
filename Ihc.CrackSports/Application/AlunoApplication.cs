using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.WebApp.Application.Interfaces;
using Ihc.CrackSports.WebApp.Models.Usuarios;
using System.Runtime.InteropServices;

namespace Ihc.CrackSports.WebApp.Application
{
    public class AlunoApplication : IAlunoApplication
    {
        private readonly IAlunoService _alunoService;
        private readonly IClubService _clubService;

        public AlunoApplication(IAlunoService alunoService, IClubService clubService)
        {
            _alunoService = alunoService;
            _clubService = clubService;
        }


        public async Task<MinhaContaViewModel> GetAlunoViewModel(long idUsuario)
        {
            var result = new MinhaContaViewModel();

            result.DadosAluno = await _alunoService.GetByIdUsuario(idUsuario);
            result.InformarClub(await _clubService.ObterById(result.DadosAluno.IdClub));

            return result;
        }
    }
}
