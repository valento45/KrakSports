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
    public interface IResponsavelCommand
    {
        Task<CadastroResponse> InsertOrUpdate(Responsavel request);
        Task<CadastroResponse> ExcluirResponsavel(long idResponsavel);
        Task<CadastroResponse> ExcluirResponsavelByIdAluno(long idAluno);
        Task<List<Responsavel>> ObterResponsavelAluno(long idAluno);
    }
}
