using Ihc.CrackSports.Core.Objetos.Clube.Dto;
using Ihc.CrackSports.Core.Objetos.Enums;
using Ihc.CrackSports.Core.Objetos.Notifications.Base;
using Ihc.CrackSports.Core.Objetos.Notifications.Dto;
using Ihc.CrackSports.Core.Repositorys.Base;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using Ihc.CrackSports.Core.Requests.Notifications;
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

        public async Task<bool> ExcluirNotificacoes(NotificationRequest request)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
