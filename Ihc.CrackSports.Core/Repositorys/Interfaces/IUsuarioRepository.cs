using Ihc.CrackSports.Core.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Repositorys.Interfaces
{
    public interface IUsuarioRepository
    {

        Task<bool> Incluir(CadastroRequest request);
        Task<bool> Atualizar(CadastroRequest request);
        Task<bool> Excluir(long idAluno);
        Task<string> GetMessage();
    }
}
