using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Requests.Aluno;
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
        Task<Usuario?> ObterPorUserName(string userName);
        Task<Usuario> GetById(long id);
        Task<string> GetMessage();

        Task<IEnumerable<Usuario>> GetAll();
        Task<IEnumerable<Usuario>> GetAllAdministradores();




        Task<bool> AtualizarUsuario(Usuario usuario);
    }
}
