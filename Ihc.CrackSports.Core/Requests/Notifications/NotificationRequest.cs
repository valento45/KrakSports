using Ihc.CrackSports.Core.Objetos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Requests.Notifications
{
    public class NotificationRequest
    {
        public long IdUsuario { get; set; }
        public TipoUsuario  TipoUsuario { get; set; }

        public NotificationRequest(long idUser, int tipoUsuario)
        {
            IdUsuario = idUser;
            TipoUsuario = (TipoUsuario)tipoUsuario;
        }
    }
}
