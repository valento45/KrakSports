using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Requests.Aluno;
using Ihc.CrackSports.Core.Responses.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Services.Interfaces
{
    public interface IAlunoService
    {
        Task<CadastroResponse> InsertOrUpdate(CadastroRequest Aluno, long idUsuario);
        Task<bool> ExcluirAluno(long idAluno);
        Task<List<Aluno>> ObterAlunosPorClub(long idClub);
        Task<List<Aluno>> ObterAlunoPorNome(string nome);
        Task<List<Aluno>> ObterAlunoPorDocumento(string documento);
        Task<IEnumerable<Aluno>> ObterTodosAluno(int limite = 0);
        Task<Aluno> GetById(long idAluno);
        Task<Aluno> GetByIdUsuario(long idUser);
        Task<Aluno> ObterAlunoPorCpf(long cpf);
        Task<CadastroResponse> UpdateDadosGerais(Aluno aluno);
        Task<CadastroResponse> UpdateResponsavelEndereco(Aluno aluno);
        Task<bool> AtualizarCamisa(Aluno aluno);
        Task<bool> EnviarSolicitacaoClub(long idAluno, long idClub);
    }
}
