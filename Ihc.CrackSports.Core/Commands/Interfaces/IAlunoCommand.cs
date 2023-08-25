using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Requests;
using Ihc.CrackSports.Core.Responses.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Commands.Interfaces
{
    public interface IAlunoCommand
    {
        Task<CadastroResponse> InsertOrUpdate(Aluno Aluno);
        Task<bool> ExcluirAluno(long idAluno);
        Task<List<Aluno>> ObterAlunosPorClub(long idClub);
        Task<List<Aluno>> ObterAlunoPorNome(string nome);       
        Task<List<Aluno>> ObterAlunoPorDocumento(string documento);
        Task<Aluno> GetById(long  idAluno);
        Task<Aluno?> GetByIdUsuario(long idUser);
        Task<Aluno> ObterAlunoPorCpf(long cpf);
        Task<CadastroResponse> UpdateDadosGerais(Aluno Aluno);
        Task<CadastroResponse> UpdateDadosResponsavel(Aluno Aluno);
    }
}
