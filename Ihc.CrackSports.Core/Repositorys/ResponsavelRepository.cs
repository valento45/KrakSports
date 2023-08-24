using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Repositorys.Base;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Repositorys
{
    public class ResponsavelRepository : RepositoryBase, IResponsavelRepository
    {
        public ResponsavelRepository(IDbConnection connection) : base(connection)
        {
            
        }

        public async Task<bool> Inserir(Responsavel responsavel)
        {
            string query = "INSERT INTO sys.responsavel_aluno_tb (id_aluno, nome_responsavel, documento_responsavel, cpf_responsavel, grau_parentesco)" +
                " values (@id_aluno, @nome_responsavel, @documento_responsavel, @cpf_responsavel, @grau_parentesco) returning id_responsavel;";

            NpgsqlCommand cmd = new NpgsqlCommand(query);
            cmd.Parameters.AddWithValue(@"id_aluno", responsavel.IdAluno);
            cmd.Parameters.AddWithValue(@"nome_responsavel", responsavel.Nome);
            cmd.Parameters.AddWithValue(@"documento_responsavel", responsavel.Documento);
            cmd.Parameters.AddWithValue(@"cpf_responsavel", responsavel.CpfCnpj);
            cmd.Parameters.AddWithValue(@"grau_parentesco", responsavel.GrauParentesco);
          
          

            var result = await base.ExecuteScalarAsync(cmd);
            long codigo;
            if (long.TryParse(result.ToString(), out codigo))
            {
                responsavel.Id = codigo;
                return codigo > 0;
            }
            else
                return false;
        }


        public async Task<bool> Atualizar(Responsavel responsavel)
        {
            string query = "update sys.responsavel_aluno_tb set id_aluno = @id_aluno, nome_responsavel = @nome_responsavel, documento_responsavel = @documento_responsavel," +
                " cpf_responsavel = @cpf_responsavel, grau_parentesco = @grau_parentesco) where id_responsavel = @id_responsavel";


            var result = await base.ExecuteAsync(query, new
            {
                id_aluno = responsavel.IdAluno, 
                nome_responsavel = responsavel.Nome,
                documento_responsavel = responsavel.Documento,
                cpf_responsavel = responsavel.CpfCnpj,
                grau_parentesco = responsavel.GrauParentesco
            });

            return result ;
        }

        public async Task<bool> Excluir(long idResponsavel)
        {
            string query = "delete from sys.responsavel_aluno_tb where id_responsavel = " + idResponsavel;
            return await base.ExecuteAsync(query);
        }

        public async Task<bool> ExcluirByIdAluno(long idAluno)
        {
            string query = "delete from sys.responsavel_aluno_tb where id_aluno = " + idAluno;
            return await base.ExecuteAsync(query);
        }


        public async Task<List<Responsavel>> ObterByIdAluno(long idAluno)
        {
            string query = "select id_responsavel as Id, id_aluno as IdAluno, nome_responsavel as Nome, documento_responsavel as Documento, " +
                $"cpf_responsavel as CpfCnpj, grau_parentesco as GrauParentesco" +
                $" from sys.responsavel_aluno_tb  where id_aluno = {idAluno}";

            var result = await base.QueryAsync<Responsavel>(query);

            return result;
        }

        public Task<List<Responsavel>> ObterByDocumento(string rg)
        {
            throw new NotImplementedException();
        }

        public Task<List<Responsavel>> ObterByCpf(long cpf)
        {
            throw new NotImplementedException();
        }

       
    }
}
