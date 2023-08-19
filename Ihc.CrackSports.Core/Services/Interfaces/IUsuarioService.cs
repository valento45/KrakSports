using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Requests;
using Ihc.CrackSports.Core.Responses.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Services.Interfaces
{
    public interface IUsuarioService
    {

        Task<CadastroResponse> InsertOrUpdate(CadastroRequest request);      
        Task<CadastroResponse> Excluir(long idAluno);
        Task<Usuario> ObterPorUserName(string userName);
    }
}
