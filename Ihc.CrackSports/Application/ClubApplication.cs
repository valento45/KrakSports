using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.WebApp.Application.Interfaces;
using Ihc.CrackSports.WebApp.Models.Clube;
using Ihc.CrackSports.WebApp.Models.Usuarios;

namespace Ihc.CrackSports.WebApp.Application
{
    public class ClubApplication : IClubApplication
    {
        private readonly IClubService _clubService;
        private readonly IAlunoService _alunoService;

        public ClubApplication(IClubService clubService, IAlunoService alunoService)
        {
            _clubService = clubService;
            _alunoService = alunoService;
        }


        public async Task<ClubViewModel> GetClubViewModel(long idUsuario)
        {
            var result = new ClubViewModel();

            result.DadosClub = await _clubService.ObterByIdUsuario(idUsuario) ?? new Core.Objetos.Clube.Club();
            var atletas = await _alunoService.ObterAlunosPorClub(result.DadosClub.Id);

            var paginacaoAluno = new Core.Utils.Paginacoes.Paginacao<Aluno>(atletas.AsQueryable(), 1, 10);

            result.Atletas.SetInstancePaginacaoAluno(paginacaoAluno);

            return result;
        }

        public async Task<ClubViewModel> GetClubViewModelByIdClube(long idClube)
        {
            var result = new ClubViewModel();

            result.DadosClub = await _clubService.ObterById(idClube) ?? new Core.Objetos.Clube.Club();
            var atletas = await _alunoService.ObterAlunosPorClub(result.DadosClub.Id);

            var paginacaoAluno = new Core.Utils.Paginacoes.Paginacao<Aluno>(atletas.AsQueryable(), 1, 10);

            result.Atletas.SetInstancePaginacaoAluno(paginacaoAluno);

            return result;
        }
    }
}
