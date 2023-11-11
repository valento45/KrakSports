using Ihc.CrackSports.Core.Objetos.Colaborador;
using Ihc.CrackSports.Core.Objetos.Colaborador.Dto;
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
    public class ColaboradorRepository : RepositoryBase, IColaboradorRepository
    {
        public ColaboradorRepository(IDbConnection dbConnection) : base(dbConnection)
        {

        }

        public async Task<bool> NovoPatrocinador(Patrocinador patrocinador)
        {
            string query = "INSERT INTO sys.patrocinador_tb(nome_razaosocial, email, is_pj, cpf_cnpj, telefone, celular, logotipo_base64, mensagem, status, instagram_url, linkedin_url, site_url)" +
                " values (@nome_razaosocial, @email, @is_pj, @cpf_cnpj, @telefone, @celular, @logotipo_base64, @mensagem, @status, @instagram_url, @linkedin_url, @site_url) returning id_patrocinador";

            NpgsqlCommand cmd = new NpgsqlCommand(query);
            cmd.Parameters.AddWithValue(@"nome_razaosocial", patrocinador.NomeOuRazaoSocial);
            cmd.Parameters.AddWithValue(@"email", patrocinador.Email);
            cmd.Parameters.AddWithValue(@"is_pj", patrocinador.IsPj);
            cmd.Parameters.AddWithValue(@"cpf_cnpj", patrocinador.CpfCnpj);
            cmd.Parameters.AddWithValue(@"telefone", patrocinador.Telefone);
            cmd.Parameters.AddWithValue(@"celular", patrocinador.Celular);
            cmd.Parameters.AddWithValue(@"logotipo_base64", patrocinador.LogoTipoBase64);
            cmd.Parameters.AddWithValue(@"mensagem", patrocinador.Mensagem);
            cmd.Parameters.AddWithValue(@"status", (int)patrocinador.Status);
            cmd.Parameters.AddWithValue(@"instagram_url", patrocinador.LinkInstagram);
            cmd.Parameters.AddWithValue(@"linkedin_url", patrocinador.LinkLinkedin);
            cmd.Parameters.AddWithValue(@"site_url", patrocinador.LinkSite);
            var result = await base.ExecuteScalarAsync(cmd);

            if (result != null && int.Parse(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> AtualizarPatrocinador(Patrocinador patrocinador)
        {
            string query = "update sys.patrocinador_tb set nome_razaosocial = @nome_razaosocial, email = @email, is_pj = @is_pj, cpf_cnpj = @cpf_cnpj, telefone = @telefone, " +
                "celular = @celular, mensagem = @mensagem, logotipo_base64 = @logotipo_base64, status = @status, observacoes = @observacoes, instagram_url = @instagram_url, " +
                "linkedin_url = @linkedin_url, site_url = @site_url, ordem_apresentacao = @ordem_apresentacao where id_patrocinador = @id_patrocinador  ";

            NpgsqlCommand cmd = new NpgsqlCommand(query);
            cmd.Parameters.AddWithValue(@"id_patrocinador", patrocinador.IdPatrocinador);
            cmd.Parameters.AddWithValue(@"nome_razaosocial", patrocinador.NomeOuRazaoSocial);
            cmd.Parameters.AddWithValue(@"email", patrocinador.Email);
            cmd.Parameters.AddWithValue(@"is_pj", patrocinador.IsPj);
            cmd.Parameters.AddWithValue(@"cpf_cnpj", patrocinador.CpfCnpj);
            cmd.Parameters.AddWithValue(@"telefone", patrocinador.Telefone);
            cmd.Parameters.AddWithValue(@"celular", patrocinador.Celular);
            cmd.Parameters.AddWithValue(@"mensagem", patrocinador.Mensagem);
            cmd.Parameters.AddWithValue(@"logotipo_base64", patrocinador.LogoTipoBase64);
            cmd.Parameters.AddWithValue(@"status", (int)patrocinador.Status);
            cmd.Parameters.AddWithValue(@"observacoes", patrocinador.Observacoes);
            cmd.Parameters.AddWithValue(@"instagram_url", patrocinador.LinkInstagram);
            cmd.Parameters.AddWithValue(@"linkedin_url", patrocinador.LinkLinkedin);
            cmd.Parameters.AddWithValue(@"site_url", patrocinador.LinkSite);
            cmd.Parameters.AddWithValue(@"ordem_apresentacao", patrocinador.OrdemApresentacao);

            var result = await base.ExecuteCommand(cmd);

            return result;
        }

        public async Task<bool> ExcluirPatrocinador(long idPatrocinador)
        {
            string query = "delete from sys.patrocinador_tb where id_patrocinador = " + idPatrocinador;

            return await base.ExecuteAsync(query);

        }

        public async Task<IEnumerable<Patrocinador>> GetAll()
        {
            string query = "select * from sys.patrocinador_tb ";


            var result = await base.QueryAsync<PatrocinadorDto>(query);
            return result?.Select(x => x.ToPatrocinador()) ?? new List<Patrocinador>();
        }

        public async Task<IEnumerable<Patrocinador>> GetAllAtivos()
        {
            string query = "select * from sys.patrocinador_tb where status = 1 order by ordem_apresentacao desc";


            var result = await base.QueryAsync<PatrocinadorDto>(query);
            return result?.Select(x => x.ToPatrocinador()) ?? new List<Patrocinador>();
        }

        public async Task<IEnumerable<Patrocinador>> GetAllPendentes()
        {
            string query = "select * from sys.patrocinador_tb where status = 0 ";


            var result = await base.QueryAsync<PatrocinadorDto>(query);
            return result?.Select(x => x.ToPatrocinador()) ?? new List<Patrocinador>();
        }

    }
}
