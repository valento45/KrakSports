using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Objetos.Clube.Dto;
using Ihc.CrackSports.Core.Repositorys.Base;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using Ihc.CrackSports.Core.Responses.Usuarios;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Repositorys
{
    public class ClubRepository : RepositoryBase, IClubRepository
    {
        public ClubRepository(IDbConnection connection) : base(connection)
        {

        }

        public async Task<bool> Incluir(Club club)
        {
            string query = "insert into sys.club_tb (nome_fantasia, cidade, uf, imagem_base64, id_usuario, data_fundacao, nome_presidente, " +
                "is_verificado, sobre_o_clube, celular)" +
                " values (@nome_fantasia, @cidade, @uf, @imagem_base64, @id_usuario, @data_fundacao, @nome_presidente, @is_verificado," +
                " @sobre_o_clube, @celular) returning id_club;;";
            NpgsqlCommand cmd = new NpgsqlCommand(query);

            cmd.Parameters.AddWithValue(@"id_usuario", club?.IdUsuario ?? null);
            cmd.Parameters.AddWithValue(@"nome_fantasia", club.Nome ?? "");
            cmd.Parameters.AddWithValue(@"cidade", club.Endereco.Cidade ?? "");
            cmd.Parameters.AddWithValue(@"uf", club.Endereco.UF ?? "");
            cmd.Parameters.AddWithValue(@"imagem_base64", club.ImagemBase64 ?? "");
            cmd.Parameters.AddWithValue(@"is_verificado", club?.IsVerificado ?? false);
            cmd.Parameters.AddWithValue(@"data_fundacao", club?.DataFundacao ?? new DateTime());
            cmd.Parameters.AddWithValue(@"nome_presidente", club?.NomePresidente ?? "");
            cmd.Parameters.AddWithValue(@"sobre_o_clube", club?.SobreOClube ?? "");
            cmd.Parameters.AddWithValue(@"celular", club?.Celular ?? "");
            

            var result = await base.ExecuteScalarAsync(cmd);
            long codigo;
            if (long.TryParse(result.ToString(), out codigo))
            {
                club.Id = codigo;
                return codigo > 0;
            }
            else
                return false;
        }

        public async Task<bool> Atualizar(Club club)
        {
            string query = "update sys.club_tb set nome_fantasia = @nome_fantasia, cidade = @cidade, uf = @uf, imagem_base64 = @imagem_base64," +
                " data_fundacao = @data_fundacao, nome_presidente = @nome_presidente, sobre_o_clube = @sobre_o_clube," +
                " is_verificado = @is_verificado, celular = @celular where id_club = @id_club";

            NpgsqlCommand cmd = new NpgsqlCommand(query);

            cmd.Parameters.AddWithValue(@"id_club", club.Id);
            cmd.Parameters.AddWithValue(@"nome_fantasia", club.Nome);
            cmd.Parameters.AddWithValue(@"cidade", club.Endereco.Cidade);
            cmd.Parameters.AddWithValue(@"uf", club.Endereco.UF);
            cmd.Parameters.AddWithValue(@"imagem_base64", club.ImagemBase64 ?? "");
            cmd.Parameters.AddWithValue(@"id_usuario", club?.IdUsuario ?? null);
            cmd.Parameters.AddWithValue(@"data_fundacao", club?.DataFundacao ?? null);
            cmd.Parameters.AddWithValue(@"nome_presidente", club?.NomePresidente ?? "");
            cmd.Parameters.AddWithValue(@"sobre_o_clube", club?.SobreOClube ?? "");
            cmd.Parameters.AddWithValue(@"is_verificado", club?.IsVerificado ?? false);
            cmd.Parameters.AddWithValue(@"celular", club?.Celular ?? "");

            return await base.ExecuteCommand(cmd);
        }

        public async Task<bool> Excluir(long idClub)
        {
            string query = "delete from sys.club_tb where id_club = " + idClub;
            return await base.ExecuteAsync(query);
        }



        public async Task<Club?> ObterById(long idClub)
        {
            string query = "select * from sys.club_tb where id_club =" + idClub;

            var result = await base.QueryAsync<ClubDto>(query);

            return result?.Select(x => x.ToClub())?.FirstOrDefault() ?? null;
        }

        public async Task<List<Club>?> ObterByNome(string nome, int limite = 0)
        {
            string query = $"select * from sys.club_tb where UPPER(nome_fantasia) like UPPER('%{nome}%')";

            if (limite > 0)
                query += $" LIMIT {limite}";

            var result = await base.QueryAsync<ClubDto>(query);

            return result?.Select(x => x.ToClub()).ToList();
        }

        public async Task<Club?> ObterByIdUsuario(long idUsuario)
        {
            string query = "select * from sys.club_tb where id_usuario =" + idUsuario;

            var result = await base.QueryAsync<ClubDto>(query);

            return result?.Select(x => x.ToClub())?.FirstOrDefault() ?? null;
        }

        public async Task<bool> AceitarAlunoClub(SolicitacaoAlunoClub solicitacao)
        {
            //string query = "insert into sys.aluno_club_tb (id_aluno, id_club, data_ingresso) values (@id_aluno, @id_club, @data_ingresso)";

            //var result = await ExecuteAsync(query, new
            //{
            //    id_aluno = solicitacao.IdAluno,
            //    id_club = solicitacao.IdClub,
            //    data_ingresso = DateTime.Now
            //});
            string query = "update sys.aluno_tb set id_club = @id_club where id_aluno = @id_aluno";
            var result = await ExecuteAsync(query, new
            {
                id_aluno = solicitacao.IdAluno,
                id_club = solicitacao.IdClub
            });

            return result;
        }

        public async Task<IEnumerable<Club>> ObterTodos(int limite, bool exibeSomenteAtivos = false)
        {
            string query = "select * from sys.club_tb";

            if (exibeSomenteAtivos)
                query += " where is_verificado = true";

            if (limite > 0)
                query += " limit " + limite;

            var result = await QueryAsync<ClubDto>(query);

            return result?.Select(p => p.ToClub()) ?? new List<Club>();
        }
    }
}
