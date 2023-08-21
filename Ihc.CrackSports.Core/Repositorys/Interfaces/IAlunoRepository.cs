using Ihc.CrackSports.Core.Objetos.Alunos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Repositorys.Interfaces
{
    public interface IAlunoRepository
    {

        Task<bool> Inserir(Aluno aluno);
        Task<bool> Atualizar(Aluno aluno);
        Task<bool> Excluir(long idAluno);

        Task<IEnumerable<Aluno>> ObterAlunosByIdClub(long idClub);
        Task<Aluno> ObterAlunoById(long idAluno);
        Task<Aluno?> ObterAlunoByIdUsuario(long idUser);
        Task<IEnumerable<Aluno>> ObterAlunoByCpf(long cpf);
        Task<IEnumerable<Aluno>> ObterAlunoByNome(string nome);
        Task<IEnumerable<Aluno>> ObterTodosAluno(int limite);
        Task<bool> AtualizarDadosGerais(Aluno aluno);
    }
}
