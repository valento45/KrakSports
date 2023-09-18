using Ihc.CrackSports.Core.Commands.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Notifications.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly INotificationCommand _notificationCommand;

        public NotificationHub(INotificationCommand notificationCommand)
        {
            _notificationCommand = notificationCommand;
        }


        public async Task SendNotification(string user, string title, string message, string link)
        {
            await _notificationCommand.TrataNotificacao(user, title, message, link);
            await Clients.User(user).SendAsync("refreshNotification", title, message, link);
        }

        public async Task SendSolicitacaoAlunoToClub(long idALuno, long idClub)
        {
            await _notificationCommand.TrataEnvioSolicitacaoAlunoToClub(idALuno, idClub);
            await Clients.User(idClub.ToString()).SendAsync("refreshNotification", "Solicitação de aluno", "Uma solicitação de aluno foi recebida.", "http://teste/solicitacao/1123");
        }


        public async Task SendMessage(long idAluno, string message)
        {

        }

    }

    
}
