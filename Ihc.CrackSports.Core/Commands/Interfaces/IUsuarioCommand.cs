using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Requests.Aluno;
using Ihc.CrackSports.Core.Responses.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Commands.Interfaces
{
    public interface IUsuarioCommand
    {
        Task<CadastroResponse> InsertOrUpdate(CadastroRequest request);
        Task<CadastroResponse> ExcluirUsuario(long idAluno);
        Task<Usuario> ObterPorUserName(string userName);
        Task<Usuario> GetById(long id);
    }
}
