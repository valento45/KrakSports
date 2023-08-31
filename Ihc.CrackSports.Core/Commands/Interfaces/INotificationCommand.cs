using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Commands.Interfaces
{
    public interface INotificationCommand
    {
        Task TrataNotificacao(string user, string title, string message, string link);
        Task TrataEnvioSolicitacaoAlunoToClub(long idAluno, long idClub);
    }
}
