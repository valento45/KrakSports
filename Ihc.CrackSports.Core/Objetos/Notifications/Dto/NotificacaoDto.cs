using Ihc.CrackSports.Core.Objetos.Enums;
using Ihc.CrackSports.Core.Objetos.Notifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Notifications.Dto
{
    public class NotificacaoDto
    {
        public long id_notificacao { get; set; }
        public long id_aluno { get; set; }
        public long id_club { get; set; }
        public DateTime data_notificacao { get; set; }
        public bool is_visto { get; set; }
        public int tipo_usuario { get; set; }
        public string notificacao { get; set; }
        public string imagem_notificacao { get; set; }
        public string link_redirect { get; set; }


        public NotificationBase ToNotificationBase()
        {
            return new NotificationBase()
            {
                IdNotificacao = id_notificacao,
                IdAluno = id_aluno,
                IdClube = id_club,
                DataNotificacao = data_notificacao,
                IsVisto = is_visto,
                Tipo = TipoNotificacao.Outros,
                TipoUsuario = (TipoUsuario)tipo_usuario,
                Notificacao = notificacao,
                ImagemNotificacao = imagem_notificacao,
                LinkRedirect = link_redirect
            };
        }
    }
}
