using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Objetos.Notifications.Base;
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
        private readonly IAlunoCommand _alunoCommand;
        private readonly IClubCommand _clubCommand;
        private readonly IUsuarioContext _usuarioContext;

        public NotificationHub(INotificationCommand notificationCommand, IAlunoCommand alunoCommand, IClubCommand clubCommand, IUsuarioContext usuarioContext)
        {
            _notificationCommand = notificationCommand;
            _alunoCommand = alunoCommand;
            _clubCommand = clubCommand;
            _usuarioContext = usuarioContext;
        }


        public async Task SendSolicitacaoAlunoToClub(long idUsuario, long idClub)
        {

            var aluno = await _alunoCommand.GetByIdUsuario(idUsuario);
            var UserClub = await _clubCommand.ObterById(idClub);

            await _notificationCommand.TrataEnvioSolicitacaoAlunoToClub(aluno.Id, idClub);

            await Clients.User(UserClub.IdUsuario.ToString()).SendAsync("refreshNotification", "Solicitação de aluno", "Uma solicitação de aluno foi recebida.", "http://teste/solicitacao/1123");
        }

        public async Task SendNotification(string user, string title, string message, string link)
        {
            await _notificationCommand.TrataNotificacao(user, title, message, link);
            await Clients.User(user).SendAsync("refreshNotification", title, message, link);
        }

        public async Task AceitarSolicitacaoAluno(long idClub, long idAluno)
        {
            var solicitacao = new SolicitacaoAlunoClub { IdClub = idClub, IdAluno = idAluno };

            var success = await _notificationCommand.AceitarSolicitacao(solicitacao);

            if (!success)
                throw new Exception("Não foi possível aceitar a solicitação no momento, tente mais tarde.");
            else
            {

                solicitacao.From = await _alunoCommand.GetById(idAluno);
                solicitacao.To = await _clubCommand.ObterById(idClub);
                solicitacao.Notificacao = $"O Clube {solicitacao.To.Nome} Aceitou sua solicitação, agora você faz parte deste clube.";

                if (solicitacao.To?.HasImagem() ?? false)
                    solicitacao.ImagemNotificacao = solicitacao.To.ImagemBase64;

                if (solicitacao.To != null)
                    solicitacao.LinkRedirect = $"../Club/ApresentacaoClub?idClub={solicitacao.To.Id}";

                await _notificationCommand.NotificarSolicitacaoAlunoAceito(solicitacao);

                ///Informar Aluno em tempo Real				
                await Clients.User(solicitacao.From.IdUsuario.ToString()).SendAsync("refreshNotification", solicitacao?.To?.Nome, "", solicitacao?.Notificacao ?? "", solicitacao?.LinkRedirect ?? "");
            }

        }

        public async Task ExcluirSolicitacaoAluno(long idNotificacao)
        {

            var success = await _notificationCommand.RemoverSolicitacao(idNotificacao);

            if (!success)
                throw new Exception("Não foi possível excluir a solicitação no momento, tente mais tarde.");
        }

    }


}
