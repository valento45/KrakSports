using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Base.Pessoas;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Objetos.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Notifications.Base
{
    public class NotificationBase
    {


        public long IdNotificacao { get; set; }
        public TipoNotificacao Tipo { get; set; }
        public DateTime DataNotificacao { get; set; }
        public string Notificacao { get; set; }
        public bool IsVisto { get; set; }
        public string LinkRedirect { get; set; }
        public string ImagemNotificacao { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public long IdAluno { get; set; }
        public long IdClube { get; set; }



        public NotificationBase()
        {

        }

        public NotificationBase(DataRow dr)
        {

        }
    }
}


