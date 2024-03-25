using Ihc.CrackSports.Core.Objetos.Notifications.Base;
using Ihc.CrackSports.Core.Requests.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Repositorys.Interfaces
{
    public interface INotificacaoRepository
    {
        Task<IEnumerable<NotificationBase>> ObterTodasNotificacoesAluno(long idAluno, int limite = 0);
        Task<IEnumerable<NotificationBase>> ObterTodasNotificacoesClube(long idClube, int limite = 0);
        Task<IEnumerable<NotificationBase>> ObterTodasNotificacoesAdministrador(long idUser, int limite = 0);

        Task<bool> ExcluirNotificacoes(NotificationRequest request);


        Task<bool> ExcluirNotificacoesClube(long idClube);
        Task<bool> IncluirNotificacao(NotificationBase notification);

        Task<bool> AtualizarNotificacao(NotificationBase notification);
    }
}
