using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Base.Pessoas;
using Ihc.CrackSports.Core.Objetos.Notifications;
using Ihc.CrackSports.Core.Objetos.Notifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Clube
{
    public class SolicitacaoAlunoClub : NotificationBase
    {
        private Aluno _from { get; set; }
        private Club _to { get; set; }

        public long IdSolicitacao { get; set; }
        public long IdAluno { get; set; }
        public long IdClub { get; set; }
        public bool IsAceito { get; set; }

        public  Aluno From
        {
            get
            {

                return _from;
            }
            set
            {
                _from = value ;
            }
        }

        public  Club To
        {
            get
            {

                return _to;
            }
            set
            {
                _to = value ;
            }
        }

        public SolicitacaoAlunoClub()
        {

        }


        public void TratarNotificacao()
        {

            Notificacao = $"enviou uma solicitação para participar do Clube. ";
        }

        public void InformarAluno(Aluno from)
        {
            From = from;
        }

        public NotificationBase ToBase()
        {
            NotificationBase notification;
            if (Tipo == TipoNotificacao.SolicitacaoAluno)
            {
                notification = new SolicitacaoAlunoClub();


               
                notification.IsVisto = IsVisto;
                notification .IdNotificacao = IdNotificacao;
                notification.DataNotificacao = DataNotificacao;
                notification.Notificacao = Notificacao;
                notification.LinkRedirect = LinkRedirect;

                if(notification is SolicitacaoAlunoClub soli)
                {
                    soli.IdSolicitacao = IdSolicitacao;
                    soli.IdAluno = IdAluno;
                    soli.IdClub = IdAluno;
                    soli.IsAceito = IsAceito;
                    soli.From = From;
                    soli.To = To;
                }
                

                return notification;
            }
            else
            {
                notification = new NotificationBase();

                
                notification.IsVisto = IsVisto;
                notification.IdNotificacao = IdNotificacao;
                notification.DataNotificacao = DataNotificacao;
                notification.Notificacao = Notificacao;
                notification.LinkRedirect = LinkRedirect;

                return notification;
            }

        }
    }
}
