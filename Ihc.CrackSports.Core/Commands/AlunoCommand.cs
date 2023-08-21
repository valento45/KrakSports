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
            if (Aluno.Id > 0)
            {
                sucesso = await _alunoRepository.Atualizar(Aluno);
            }
            else
            {
                sucesso = await _alunoRepository.Inserir(Aluno);
            }
            return sucesso ? new CadastroResponse { StatusCode = 200 } : new CadastroResponse { StatusCode = 500, Message = "Erro ao salvar os dados do aluno !" };

        }

        public async Task<CadastroResponse> UpdateDadosGerais(Aluno Aluno)
        {
            bool sucesso;
            if (Aluno.Id > 0)
            {
                sucesso = await _alunoRepository.AtualizarDadosGerais(Aluno);
            }
            else
            {
                throw new InvalidOperationException("Impossivel atualizar os dados gerais pois o aluno nao possui Id ");
            }
            return sucesso ? new CadastroResponse { StatusCode = 200 } : new CadastroResponse { StatusCode = 500, Message = "Erro ao salvar os dados do aluno !" };

        }

        public async Task<bool> ExcluirAluno(long idAluno)
        {
            return await _alunoRepository.Excluir(idAluno);
        }

        public async Task<Aluno> GetById(long idAluno)
        => await _alunoRepository.ObterAlunoById(idAluno);


        public async Task<Aluno?> ObterAlunoPorCpf(long cpf)
        {
            var result = await _alunoRepository.ObterAlunoByCpf(cpf);
            return result.FirstOrDefault();
        }

        public async Task<List<Aluno>> ObterAlunoPorDocumento(string documento) => throw new NotImplementedException();


        public async Task<List<Aluno>> ObterAlunoPorNome(string nome)
        {
            var result = await _alunoRepository.ObterAlunoByNome(nome);
            return result?.ToList();
        }

        public async Task<List<Aluno>> ObterAlunosPorClub(long idClub)
        {
            var result = await _alunoRepository.ObterAlunosByIdClub(idClub);
            return result.ToList();
        }

        public async Task<Aluno?> GetByIdUsuario(long idUser)
        {
            return await _alunoRepository.ObterAlunoByIdUsuario(idUser);
        }
    }
}
