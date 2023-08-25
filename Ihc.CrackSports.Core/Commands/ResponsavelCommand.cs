using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using Ihc.CrackSports.Core.Responses.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Commands
{
    public class ResponsavelCommand : IResponsavelCommand
    {
        protected readonly IResponsavelRepository _responsavelRepository;

        public ResponsavelCommand(IResponsavelRepository responsavelRepository)
        {
            _responsavelRepository = responsavelRepository;
        }

        public async Task<CadastroResponse> InsertOrUpdate(Responsavel request)
        {
            bool success;

            if (request.Id > 0)            
                success = await _responsavelRepository.Atualizar(request, request.IdAluno);
            
            else            
                success = await _responsavelRepository.Inserir(request);            

            return success ? new CadastroResponse { StatusCode = 200 } : new CadastroResponse { StatusCode = 500, Message = "Erro ao excluir o responsável ! Tente mais tarde." };
        }

        public Task<CadastroResponse> ExcluirResponsavel(long idResponsavel)
        {
            throw new NotImplementedException();
        }

        public Task<CadastroResponse> ExcluirResponsavelByIdAluno(long idAluno)
        {
            throw new NotImplementedException();
        }



        public Task<List<Responsavel>> ObterResponsavelAluno(long idAluno)
        {
            throw new NotImplementedException();
        }
    }
}
