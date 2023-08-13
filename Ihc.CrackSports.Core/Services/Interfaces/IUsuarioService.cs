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

        Task<CadastroUsuarioResponse> InsertOrUpdate(CadastroRequest request);      
        Task<CadastroUsuarioResponse> Excluir(long idAluno);
    }
}
