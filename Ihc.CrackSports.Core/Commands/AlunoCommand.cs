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
    public class AlunoCommand : IAlunoCommand
    {
        protected readonly IAlunoRepository _alunoRepository;

        public AlunoCommand(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public async Task<CadastroResponse> InsertOrUpdate(Aluno Aluno)
        {
            bool sucesso;
            if(Aluno.Id > 0)
            {
                sucesso = await _alunoRepository.Atualizar(Aluno);
            }
            else
            {
                sucesso = await _alunoRepository.Inserir(Aluno);
            }
            return sucesso ? new CadastroResponse { StatusCode = 200 } : new CadastroResponse { StatusCode = 500, Message = "Erro ao salvar os dados do aluno !" };
           
        }

        public async Task<bool> ExcluirAluno(long idAluno)
        {
            return await _alunoRepository.Excluir(idAluno);
        }

        public Task<Aluno> GetById(long idAluno)
        {
            throw new NotImplementedException();
        }

        public Task<Aluno> ObterAlunoPorCpf(long cpf)
        {
            throw new NotImplementedException();
        }

        public Task<List<Aluno>> ObterAlunoPorDocumento(string documento)
        {
            throw new NotImplementedException();
        }

        public Task<List<Aluno>> ObterAlunoPorNome(string nome)
        {
            throw new NotImplementedException();
        }

        public Task<List<Aluno>> ObterAlunosPorClub(long idClub)
        {
            throw new NotImplementedException();
        }
    }
}
