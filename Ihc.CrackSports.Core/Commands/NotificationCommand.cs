using Ihc.CrackSports.Core.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Commands
{
    public class NotificationCommand : INotificationCommand
    {
        public NotificationCommand()
        {
            
        }

        public async Task TrataEnvioSolicitacaoAlunoToClub(long idAluno, long idClub)
        {
            Console.WriteLine("bateu");
        }

        public async Task TrataNotificacao(string user, string title, string message, string link)
        {
            Console.WriteLine("bateu");
        }
    }
}
