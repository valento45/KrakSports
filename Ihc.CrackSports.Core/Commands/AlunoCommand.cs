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
        protected readonly IResponsavelRepository _responsavelRepository;

        public AlunoCommand(IAlunoRepository alunoRepository, IResponsavelRepository responsavelRepository)
        {
            _alunoRepository = alunoRepository;
            _responsavelRepository = responsavelRepository;
        }

        public async Task<CadastroResponse> InsertOrUpdate(Aluno Aluno)
        {
            bool sucesso;
            if (Aluno.Id > 0)
            {
                sucesso = await _alunoRepository.Atualizar(Aluno);

                if (Aluno.HasEditResponsavel)
                    sucesso = await _responsavelRepository.Atualizar(Aluno.Responsavel, Aluno.Id);
            }
            else
            {
                await _alunoRepository.Inserir(Aluno);
                sucesso = await _responsavelRepository.Inserir(Aluno.Responsavel);
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

        public async Task<CadastroResponse> UpdateDadosResponsavel(Aluno Aluno)
        {
            bool sucesso = false;
            if (Aluno.Id > 0)
            {
                sucesso = await _responsavelRepository.Atualizar(Aluno.Responsavel, Aluno.Id);
                sucesso = await _alunoRepository.AtualizarEndereco(Aluno);
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
        {
            var result = await _alunoRepository.ObterAlunoById(idAluno);

            result.InformarResponsavel(await _responsavelRepository.ObterByIdAluno(idAluno));

            return result;
        }


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
            var aluno =  await _alunoRepository.ObterAlunoByIdUsuario(idUser);

            aluno?.InformarResponsavel(await _responsavelRepository.ObterByIdAluno(aluno.Id));

            return aluno;
        }

        public async Task<bool> AtualizarCamisa(Aluno aluno)
        {
            return await _alunoRepository.AtualizarCamisa(aluno);
        }
    }
}
