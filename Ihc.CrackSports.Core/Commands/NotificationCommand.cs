using Ihc.CrackSports.Core.Authorization.Claims;
using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Objetos.Enums;
using Ihc.CrackSports.Core.Objetos.Notifications.Base;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using Ihc.CrackSports.Core.Requests.Clube.Solicitacoes;
using Ihc.CrackSports.Core.Requests.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Commands
{
    public class NotificationCommand : INotificationCommand
    {
        private readonly ISolicitacaoClubAlunoRepository _solicitacaoClubAlunoRepository;
        private readonly IClubCommand _clubCommand;
        private readonly IAlunoCommand _alunoCommand;
        private readonly IUsuarioContext _usuarioContext;
        private readonly INotificacaoRepository _notificacaoRepository;
        private readonly IUsuarioCommand _usuarioCommand;

        public NotificationCommand(ISolicitacaoClubAlunoRepository solicitacaoClubAlunoRepository, IClubCommand clubCommand, IAlunoCommand alunoCommand,
            IUsuarioContext usuarioContext, INotificacaoRepository notificacaoRepository, IUsuarioCommand usuarioCommand)
        {
            _solicitacaoClubAlunoRepository = solicitacaoClubAlunoRepository;
            _clubCommand = clubCommand;
            _alunoCommand = alunoCommand;
            _usuarioContext = usuarioContext;
            _notificacaoRepository = notificacaoRepository;
            _usuarioCommand = usuarioCommand;
        }



        #region METODOS PRIVADOS

        private async Task<IEnumerable<NotificationBase>> ObterTodasNotificacoesAluno(long idAluno)
        {
            var result = await _notificacaoRepository.ObterTodasNotificacoesAluno(idAluno);
            return result.OrderByDescending(x => x.DataNotificacao);
        }
        private async Task<IEnumerable<NotificationBase>> ObterTodasNotificacoesAdministrador(long idUser)
        {
            var result = await _notificacaoRepository.ObterTodasNotificacoesAdministrador(idUser);
            return result.OrderByDescending(x => x.DataNotificacao);
        }

        private async Task<IEnumerable<NotificationBase>> ObterTodasNotificacoesClube(long idClube)
        {
            var result = new List<NotificationBase>();

            try
            {
                await IncluirSolicitacoesAlunosClube(result, await this.ObterTodasSolicitacoesDoClube(idClube));
                await IncluirNotificacoesGenerics(result, await _notificacaoRepository.ObterTodasNotificacoesClube(idClube));

            }
            catch { }

            return result.OrderByDescending(x => x.DataNotificacao);
        }


        private async Task IncluirSolicitacoesAlunosClube(List<NotificationBase> include, IEnumerable<SolicitacaoAlunoClub> reference)
        {
            if (include == null) include = new List<NotificationBase>();

            foreach (var obj in reference)
            {
                obj.InformarAluno(await _alunoCommand.GetById(obj.IdAluno));
                obj.Notificacao = " Enviou uma solicitação para participar do clube.";

                include.Add(obj);
            }
        }


        private async Task IncluirNotificacoesGenerics(List<NotificationBase> include, IEnumerable<NotificationBase> reference)
        {
            if (include == null) include = new List<NotificationBase>();

            include.AddRange(reference);
        }
        #endregion





        public async Task<bool> AceitarSolicitacao(SolicitacaoAlunoClub solicitacao)
        {

            var result = await _solicitacaoClubAlunoRepository.AceitarSolicitacao(solicitacao);

            if (result)
                await this.NotificarSolicitacaoAlunoAceito(solicitacao);
            return result;
        }

        public async Task<IEnumerable<SolicitacaoAlunoClub>> ObterTodasSolicitacoesDoClube(long idClube)
        {
            return await _solicitacaoClubAlunoRepository.ObterTodasSolicitacoesDoClube(idClube);
        }

        public async Task<bool> PossuiSolicitacaoPendente(long idAluno)
        {
            return await _solicitacaoClubAlunoRepository.PossuiSolicitacaoPendente(idAluno);
        }



        public async Task<bool> RemoverSolicitacao(long idSolicitacao)
        {
            return await _solicitacaoClubAlunoRepository.RemoverSolicitacao(idSolicitacao);
        }

        public async Task TrataEnvioSolicitacaoAlunoToClub(long idAluno, long idClub)
        {
            var obj = new SolicitacaoAlunoClubRequest()
            {
                Solicitacao = new Objetos.Clube.SolicitacaoAlunoClub
                {
                    DataNotificacao = DateTime.Now,
                    IdAluno = idAluno,
                    IdClub = idClub,
                    IsAceito = false
                }
            };

            var result = await _solicitacaoClubAlunoRepository.EnviarSolicitacao(obj);


            if (result.IdSolicitacao <= 0)
                throw new Exception("Erro ao enviar solicitação !");
        }

        public async Task TrataNotificacao(string user, string title, string message, string link)
        {
            Console.WriteLine("bateu");
        }




        public async Task<IEnumerable<NotificationBase>> ObtemESetaNoContextoTodasNotificacoes(NotificationRequest request)
        {
            if (request.TipoUsuario == TipoUsuario.Club)
            {
                if (request.IdUsuario > 0)
                {
                    var club = await _clubCommand.ObterByIdUsuario(request.IdUsuario);

                    if (club?.Id > 0)
                    {
                        var result = await ObterTodasNotificacoesClube(club.Id);
                        _usuarioContext.SetNotificacoes(result.ToList());

                        return result;
                    }
                    else
                        return new List<NotificationBase>();
                }
            }
            else if (request.TipoUsuario == TipoUsuario.Aluno)
            {
                var aluno = await _alunoCommand.GetByIdUsuario(request.IdUsuario);
                var result = await this.ObterTodasNotificacoesAluno(aluno.Id);
                _usuarioContext.SetNotificacoes(result.ToList());

                return result;

            }
            else if (request.TipoUsuario == TipoUsuario.Administrador)
            {
                var result = await this.ObterTodasNotificacoesAdministrador(request.IdUsuario);
                _usuarioContext.SetNotificacoes(result.ToList());

                return result;
            }

            return new List<NotificationBase>();
        }

        public async Task<SolicitacaoAlunoClub> ObterSolicitacaoAlunoById(long idSolicitacao)
        {
            return await _solicitacaoClubAlunoRepository.ObterSolicitacaoById(idSolicitacao);
        }

        public async Task<bool> NotificarSolicitacaoAlunoAceito(SolicitacaoAlunoClub solicitacao)
        {
            await this.FillInstance(solicitacao);

            return await _solicitacaoClubAlunoRepository.NotificarSolicitacaoAlunoAceito(solicitacao);
        }


        private async Task FillInstance(SolicitacaoAlunoClub solicitacao)
        {
            if (solicitacao == null)
                throw new ArgumentNullException("Não é possível preencher as referências de um objeto vazio ou nulo.");


            solicitacao.DataNotificacao = solicitacao.DataNotificacao <= new DateTime() ? DateTime.Now : solicitacao.DataNotificacao;
            solicitacao.From = await _alunoCommand.GetById(solicitacao.IdAluno);
            solicitacao.To = await _clubCommand.ObterById(solicitacao.IdClub);
            solicitacao.Notificacao = $"O Clube {solicitacao.To.Nome} Aceitou sua solicitação, agora você faz parte deste clube.";

            if (solicitacao.To?.HasImagem() ?? false)
                solicitacao.ImagemNotificacao = solicitacao.To.ImagemBase64;

            if (solicitacao.To != null)
                solicitacao.LinkRedirect = $"../Club/ApresentacaoClub?idClub={solicitacao.To.Id}";

        }


        private async Task<NotificationBase> CriarNotificacao(Club club, string message)
        {
            var notification = new NotificationBase();

            notification.Notificacao = message;
            notification.DataNotificacao = notification.DataNotificacao <= new DateTime() ? DateTime.Now : notification.DataNotificacao;
            notification.Notificacao = message;
            notification.ImagemNotificacao = club.ImagemBase64;
            notification.LinkRedirect = $"../Club/ApresentacaoClub?idClub={club.Id}";
            notification.Tipo = Objetos.Notifications.TipoNotificacao.Outros;

            return notification;

        }


        public async Task<bool> IncluirNotificacao(NotificationBase notification)
        {
            if (notification == null)
                return false;

            return await _notificacaoRepository.IncluirNotificacao(notification);
        }

        public async Task<bool> AtualizarNotificacao(NotificationBase notification)
        {
            if (notification == null)
                return false;

            return await _notificacaoRepository.AtualizarNotificacao(notification);
        }

        public async Task<bool> NotificarAdministradoresClubeCadastrado(Club club)
        {

            var administradores = await _usuarioCommand.GetAllAdministradores();

            var message = $"O Clube {club.Nome} se cadastrou com sucesso. Vá ao painel Administrativo para liberar o acesso.";
            foreach (var admin in administradores)
            {
                var notification = await CriarNotificacao(club, message);
                notification.IdAdministrador = admin.Id;
                notification.IdClube = club.Id;
                notification.TipoUsuario = TipoUsuario.Administrador;

                 await this.IncluirNotificacao(notification);
            }
            return false;
        }

        public async Task<bool> ExcluirNotificacoesClube(long idClube)
        {
            return await _notificacaoRepository.ExcluirNotificacoesClube(idClube);
        }
    }
}
