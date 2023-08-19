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

        Task<List<Aluno>> ObterAlunosByIdClub(long idClub);
        Task<List<Aluno>> ObterAlunoById(long idAluno);
        Task<List<Aluno>> ObterAlunoByCpf(long cpf);
        Task<List<Aluno>> ObterAlunoByNome(string nome);
        Task<List<Aluno>> ObterTodosAlunoById(int limite);
    }
}
