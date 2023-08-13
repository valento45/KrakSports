using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using Ihc.CrackSports.Core.Requests;
using Ihc.CrackSports.Core.Responses.Usuarios;
using Ihc.CrackSports.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Commands
{
    public class UsuarioCommand : IUsuarioCommand
    {
        protected readonly IUsuarioRepository _usuarioRepository;


        public UsuarioCommand(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }


        public async Task<CadastroUsuarioResponse> InsertOrUpdate(CadastroRequest request)
        {
            bool sucesso = false;

            if (request.IdAluno > 0)
                sucesso = await _usuarioRepository.Atualizar(request);
            else
                sucesso = await _usuarioRepository.Incluir(request);

            return sucesso ? 
                new CadastroUsuarioResponse { Message = "", StatusCode = 200 }
                :
                new CadastroUsuarioResponse { Message = await _usuarioRepository.GetMessage(), StatusCode = 500 };
        }

        public async Task<CadastroUsuarioResponse> ExcluirUsuario(long idAluno)
        {
            var result = await _usuarioRepository.Excluir(idAluno);

            if(result)
                return new CadastroUsuarioResponse { StatusCode = 200 };

            return new CadastroUsuarioResponse { Message = await _usuarioRepository.GetMessage(), StatusCode=500 };
        }

    }
}
