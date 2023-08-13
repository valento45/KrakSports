using Ihc.CrackSports.Core.Requests;
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

        Task<CadastroUsuarioResponse> InsertOrUpdate(CadastroRequest request);
        Task<CadastroUsuarioResponse> ExcluirUsuario(long idAluno);
    }
}
