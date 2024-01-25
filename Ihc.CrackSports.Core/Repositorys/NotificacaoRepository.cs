using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Clube.Dto;
using Ihc.CrackSports.Core.Objetos.Enums;
using Ihc.CrackSports.Core.Objetos.Notifications.Base;
using Ihc.CrackSports.Core.Objetos.Notifications.Dto;
using Ihc.CrackSports.Core.Repositorys.Base;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using Ihc.CrackSports.Core.Requests.Notifications;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Repositorys
{
    public class NotificacaoRepository : RepositoryBase, INotificacaoRepository
    {
        public NotificacaoRepository(IDbConnection dbConnection) : base(dbConnection)
        {

        }

        public async Task<bool> IncluirNotificacao(NotificationBase notification)
        {
            string query = "insert into sys.notificacao_tb (id_aluno, id_club, data_notificacao, is_visto, " +
                "tipo_usuario, notificacao, imagem_notificacao, link_redirect)" +
                " values (@id_aluno, @id_club, @data_notificacao, @is_visto, @tipo_usuario, @notificacao, @imagem_notificacao," +
                " @link_redirect) returning id_notificacao";

            NpgsqlCommand cmd = new NpgsqlCommand(query);
            cmd.Parameters.AddWithValue(@"id_aluno", notification.IdAluno > 0 ? notification.IdAluno : DBNull.Value);
            cmd.Parameters.AddWithValue(@"id_club", notification.IdClube > 0 ? notification.IdClube : DBNull.Value);
            cmd.Parameters.AddWithValue(@"data_notificacao", notification.DataNotificacao);
            cmd.Parameters.AddWithValue(@"is_visto", notification.IsVisto);
            cmd.Parameters.AddWithValue(@"tipo_usuario", (int)notification.TipoUsuario);
            cmd.Parameters.AddWithValue(@"notificacao", notification.Notificacao);
            cmd.Parameters.AddWithValue(@"imagem_notificacao", string.IsNullOrEmpty(notification.ImagemNotificacao) ? string.Empty : notification.ImagemNotificacao);
            cmd.Parameters.AddWithValue(@"link_redirect", notification.LinkRedirect);

            var result = await base.ExecuteCommand(cmd);

            return result;
        }

        public async Task<bool> AtualizarNotificacao(NotificationBase notification)
        {
            string query = "UPDATE sys.notificacao_tb set id_aluno = @id_aluno, id_club = @id_club, data_notificacao = @data_notificacao," +
                " is_visto = @is_visto, tipo_usuario = @tipo_usuario, notificacao = @notificacao, imagem_notificacao = @imagem_notificacao," +
                " link_redirect = @link_redirect where id_notificacao = @id_notificacao";

            NpgsqlCommand cmd = new NpgsqlCommand(query);
            cmd.Parameters.AddWithValue(@"id_notificacao", notification.IdNotificacao);
            cmd.Parameters.AddWithValue(@"id_aluno", notification.IdAluno > 0 ? notification.IdAluno : DBNull.Value);
            cmd.Parameters.AddWithValue(@"id_club", notification.IdClube > 0 ? notification.IdClube : DBNull.Value);
            cmd.Parameters.AddWithValue(@"data_notificacao", notification.DataNotificacao);
            cmd.Parameters.AddWithValue(@"is_visto", notification.IsVisto);
            cmd.Parameters.AddWithValue(@"tipo_usuario", (int)notification.TipoUsuario);
            cmd.Parameters.AddWithValue(@"notificacao", notification.Notificacao);
            cmd.Parameters.AddWithValue(@"imagem_notificacao", string.IsNullOrEmpty(notification.ImagemNotificacao) ? string.Empty : notification.ImagemNotificacao);
            cmd.Parameters.AddWithValue(@"link_redirect", notification.LinkRedirect);

            var result = await base.ExecuteCommand(cmd);

            return result;
        }

        public async Task<bool> ExcluirNotificacoes(NotificationRequest request)
        {
            string query = "delete from sys.notificacao_tb where id_notificacao = " + request.IdNotificacao;

            var result = await base.ExecuteAsync(query);

            return result;
        }


        public async Task<IEnumerable<NotificationBase>> ObterTodasNotificacoesAluno(long idAluno, int limite = 0)
        {
            string query = $"select * from sys.notificacao_tb where id_aluno = {idAluno} AND tipo_usuario = {((int)TipoUsuario.Aluno)}";

            if (limite > 0)
                query += $" LIMIT {limite}";

            var result = await base.QueryAsync<NotificacaoDto>(query);

            return result?.Select(x => x.ToNotificationBase()).ToList();
        }

        public async Task<IEnumerable<NotificationBase>> ObterTodasNotificacoesClube(long idClube, int limite = 0)
        {
            string query = $"select * from sys.notificacao_tb where id_club = {idClube} AND tipo_usuario = {((int)TipoUsuario.Club)}";

            if (limite > 0)
                query += $" LIMIT {limite}";

            var result = await base.QueryAsync<NotificacaoDto>(query);

            return result?.Select(x => x.ToNotificationBase()).ToList();
        }
    }
}
