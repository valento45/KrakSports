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
            string query = "insert into sys.club_tb (nome_fantasia, cidade, uf, imagem_base64, id_usuario) values (@nome_fantasia, @cidade, @uf, @imagem_base64, @id_usuario) returning id_club;;";
            NpgsqlCommand cmd = new NpgsqlCommand(query);

            cmd.Parameters.AddWithValue(@"nome_fantasia", club.Nome);
            cmd.Parameters.AddWithValue(@"cidade", club.Endereco.Cidade);
            cmd.Parameters.AddWithValue(@"uf", club.Endereco.UF);
            cmd.Parameters.AddWithValue(@"imagem_base64", club.ImagemBase64 ?? "");
            cmd.Parameters.AddWithValue(@"id_usuario", club?.IdUsuario ?? null);

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
            string query = "update sys.club_tb set nome_fantasia = @nome, cidade = @cidade, uf = @uf, imagem_base64 = @imagem_base64 where id_club = @id_club";

            return await base.ExecuteAsync(query, new
            {
                id_club = club.Id,
                nome = club.Nome,
                cidade = club.Endereco.Cidade,
                uf = club.Endereco.UF,
                imagem_base64 = club.ImagemBase64
            });
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

        public async Task<bool> IncluirAlunoClub(SolicitacaoAlunoClub solicitacao)
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
    }
}
