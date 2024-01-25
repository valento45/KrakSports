using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Objetos.Notifications.Base;
using Ihc.CrackSports.Core.Requests.Notifications;
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
        Task<bool> AceitarSolicitacao(SolicitacaoAlunoClub solicitacao);
        Task<bool> RemoverSolicitacao(long idSolicitacao);
        Task<bool> PossuiSolicitacaoPendente(long idAluno);



        Task<IEnumerable<SolicitacaoAlunoClub>> ObterTodasSolicitacoesDoClube(long idClube);
        Task<IEnumerable<NotificationBase>> ObterTodasNotificacoes( NotificationRequest request);
        Task<SolicitacaoAlunoClub> ObterSolicitacaoAlunoById( long idSolicitacao);


        /// <summary>
        /// Notificacao de Solicitacao Aluno-Clube
        /// </summary>
        /// <param name="solicitacao"></param>
        /// <returns></returns>
		Task<bool> NotificarSolicitacaoAlunoAceito(SolicitacaoAlunoClub solicitacao);

        Task<bool> IncluirNotificacao(NotificationBase notification);

        Task<bool> AtualizarNotificacao(NotificationBase notification);
    }
}
