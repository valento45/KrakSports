using Ihc.CrackSports.Core.Objetos.Notifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Authorization.Context.Interfaces
{
    public interface IUsuarioContext
    {

        public Task RefreshImage();
        public void SetImage(string imagebase64);
        public string GetImage();
        public bool HasImage();


        public void SetNotificacoes(List<NotificationBase> notificacoes);
        public void SetNotificacoes(IEnumerable<NotificationBase> notificacoes);
        public Task<List<NotificationBase>> GetNotificacoes();
        public int CountNotificationsNaoVistas();
        
    }
}
