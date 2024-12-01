using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using Ihc.CrackSports.Core.Requests.Aluno;
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


       

        private async Task<bool> InserirUsuarioAsync(CadastroRequest request)
        {
            bool sucesso;

            sucesso = await _usuarioRepository.Incluir(request);           

            return sucesso;
        }

        private async Task<bool> AtualizarUsuarioAsync(CadastroRequest request)
        {
            bool sucesso;

            sucesso = await _usuarioRepository.Atualizar(request);

            return sucesso;
        }

        public async Task<CadastroResponse> InsertOrUpdate(CadastroRequest request)
        {
            var result = new CadastroResponse();
            bool sucesso = false;

            if (request.IdAluno > 0)
                sucesso = await AtualizarUsuarioAsync(request);

            else
                sucesso = await InserirUsuarioAsync(request);


            result = sucesso ? new CadastroResponse { StatusCode = 200 } : new CadastroResponse
            { Message = await _usuarioRepository.GetMessage() != string.Empty ? await _usuarioRepository.GetMessage() : "Erro ao inserir os dados do aluno !" + nameof(IAlunoRepository), StatusCode = 500 };

            return result;
        }

        public async Task<CadastroResponse> ExcluirUsuario(long idUsuario)
        {
            var result = await _usuarioRepository.Excluir(idUsuario);

            if (result)
                return new CadastroResponse { StatusCode = 200 };

            return new CadastroResponse { Message = await _usuarioRepository.GetMessage(), StatusCode = 500 };
        }

        public async Task<Usuario?> ObterPorUserName(string userName)
        {
            return await _usuarioRepository.ObterPorUserName(userName);
                
        }

        public async Task<Usuario> GetById(long id)
        {
            return await _usuarioRepository.GetById(id);
        }

        public async Task<IEnumerable<Usuario>> GetAllAdministradores()
        {
            return await _usuarioRepository.GetAllAdministradores();
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _usuarioRepository.GetAll();
        }

        public async Task<bool> AtualizarUsuario(Usuario usuario)
        {
            return await _usuarioRepository.AtualizarUsuario(usuario);
        }
    }
}
