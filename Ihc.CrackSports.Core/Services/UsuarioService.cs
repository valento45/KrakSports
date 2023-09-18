using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Requests.Aluno;
using Ihc.CrackSports.Core.Responses.Usuarios;
using Ihc.CrackSports.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Services
{
    public class UsuarioService : IUsuarioService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IUsuarioCommand _usuarioCommand;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioCommand"></param>
        public UsuarioService(IUsuarioCommand usuarioCommand)
        {
            _usuarioCommand = usuarioCommand;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CadastroResponse> InsertOrUpdate(CadastroRequest request, long idUsuario)
        {
           return await _usuarioCommand.InsertOrUpdate(request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idAluno"></param>
        /// <returns></returns>
        public async Task<CadastroResponse> Excluir(long idAluno)
        {
            return await _usuarioCommand.ExcluirUsuario(idAluno);
        }

        public async Task<Usuario> ObterPorUserName(string userName)
        {
          return await _usuarioCommand.ObterPorUserName(userName);  
        }

        public async Task<Usuario> GetById(long id)
        {
            return await _usuarioCommand.GetById(id);
        }
    }
}
