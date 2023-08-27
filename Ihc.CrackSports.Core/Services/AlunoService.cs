using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Base.Pessoas;
using Ihc.CrackSports.Core.Requests;
using Ihc.CrackSports.Core.Responses.Usuarios;
using Ihc.CrackSports.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoCommand _alunoCommand;
        private readonly IClubCommand _clubCommand;

        public AlunoService(IAlunoCommand alunoCommand, IClubCommand clubCommand)
        {
            _alunoCommand = alunoCommand;
            _clubCommand = clubCommand;
        }




        public async Task<CadastroResponse> InsertOrUpdate(CadastroRequest request, long idUsuario)
        {



            var aluno = new Aluno
            {
                Nome = request.NomeAluno,
                Documento = request.DocumentoAluno,
                CpfCnpj = request.CpfAluno,
                Endereco = request.Endereco,
                IdUsuario = idUsuario,
                DataNascimento = request.DataNascimento,
                Celular = request.CelularResponsavel,
                Telefone = request.TelefoneResponsavel,
                Email = request.Email,
                Responsavel = new Responsavel
                {
                    Nome = request.NomeResponsavel,
                    Documento = request.DocumentoResponsavel,
                    CpfCnpj = request.CpfResponsavel,
                    Endereco = request.Endereco,
                    GrauParentesco = request.GrauParentesco,
                    Celular = request.CelularResponsavel,
                    Telefone = request.TelefoneResponsavel
                }
            };

            return await _alunoCommand.InsertOrUpdate(aluno);
        }


        public async Task<CadastroResponse> UpdateDadosGerais(Aluno aluno)
        {

            return await _alunoCommand.UpdateDadosGerais(aluno);
        }

        public async Task<CadastroResponse> UpdateResponsavelEndereco(Aluno aluno)
        {
            return await _alunoCommand.UpdateDadosResponsavel(aluno);
        }

        public async Task<bool> ExcluirAluno(long idAluno)
        {
            return await _alunoCommand.ExcluirAluno(idAluno);
        }

        public async Task<Aluno> GetById(long idAluno)
        {
            return await _alunoCommand.GetById(idAluno);
        }


        public async Task<Aluno> ObterAlunoPorCpf(long cpf)
        {
            return await _alunoCommand.ObterAlunoPorCpf(cpf);
        }

        public async Task<List<Aluno>> ObterAlunoPorDocumento(string documento)
        {
            return await _alunoCommand.ObterAlunoPorDocumento(documento);
        }

        public async Task<List<Aluno>> ObterAlunoPorNome(string nome)
        {
            return await _alunoCommand.ObterAlunoPorNome(nome);
        }

        public async Task<List<Aluno>> ObterAlunosPorClub(long idClub)
        {
            return await _alunoCommand.ObterAlunosPorClub(idClub);
        }

        public async Task<Aluno?> GetByIdUsuario(long idUser)
        {
            return await _alunoCommand.GetByIdUsuario(idUser);
        }

     
    }
}
