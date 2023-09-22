using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Objetos.Notifications.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Authorization.Context
{
    public class UsuarioContext : IUsuarioContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }



        public string GetImage()
        {
            byte[] bytes;
            _httpContextAccessor.HttpContext.Session.TryGetValue("photo", out bytes);

            if (bytes != null)
            {
                string json = Encoding.UTF8.GetString(bytes);
                return json;
            }
            return "";
        }

        public bool HasImage()
        {
            return _httpContextAccessor?.HttpContext?.Session?.Keys?.Any(x => x == "photo") ?? false;
        }

        public void SetImage(string imagebase64)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(imagebase64);
            _httpContextAccessor.HttpContext.Session.Set("photo", bytes);
        }

        public void SetNotificacoes(List<NotificationBase> notificacoes)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(notificacoes));
            _httpContextAccessor.HttpContext.Session.Set("notificacoes", bytes);
        }

        public List<NotificationBase> GetNotificacoes()
        {
            byte[] bytes;
            _httpContextAccessor.HttpContext.Session.TryGetValue("notificacoes", out bytes);

            if (bytes != null)
            {
                string json = Encoding.UTF8.GetString(bytes);
                var result = JsonConvert.DeserializeObject<List<SolicitacaoAlunoClub>>(json) ?? new List<SolicitacaoAlunoClub>();

                return result.Select(x => x.ToBase()).ToList();
            }
            return new List<NotificationBase>();
        }

        public int CountNotificationsNaoVistas()
        {
            byte[] bytes;
            _httpContextAccessor.HttpContext.Session.TryGetValue("notificacoes", out bytes);

            if (bytes != null)
            {
                string json = Encoding.UTF8.GetString(bytes);
                var result = JsonConvert.DeserializeObject<List<NotificationBase>>(json) ?? new List<NotificationBase>();

                return result.Count(x => !x.IsVisto);
            }
           
            return 0;
        }

        public void SetNotificacoes(IEnumerable<NotificationBase> notificacoes)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(notificacoes.ToList()));
            _httpContextAccessor.HttpContext.Session.Set("notificacoes", bytes);
        }
    }
}
