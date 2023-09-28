﻿using Ihc.CrackSports.Core.Authorization.Claims;
using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Objetos.Notifications.Base;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using Ihc.CrackSports.Core.Requests.Clube.Solicitacoes;
using Ihc.CrackSports.Core.Requests.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public NotificationCommand(ISolicitacaoClubAlunoRepository solicitacaoClubAlunoRepository, IClubCommand clubCommand, IAlunoCommand alunoCommand, IUsuarioContext usuarioContext)
        {
            _solicitacaoClubAlunoRepository = solicitacaoClubAlunoRepository;
            _clubCommand = clubCommand;
            _alunoCommand = alunoCommand;
            _usuarioContext = usuarioContext;
        }

        public async Task<bool> AceitarSolicitacao(SolicitacaoAlunoClub solicitacao)
        {
            return await _solicitacaoClubAlunoRepository.AceitarSolicitacao(solicitacao);

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

        private async Task<IEnumerable<NotificationBase>> ObterTodasNotificacoesClube(long idClube)
        {
            var result = new List<NotificationBase>();


            var solicitacoes = await this.ObterTodasSolicitacoesDoClube(idClube);

            foreach(var obj in solicitacoes)
            {
                obj.InformarAluno(await _alunoCommand.GetById(obj.IdAluno));
                obj.TratarNotificacao();

                result.Add(obj);
            }     

          
            return result.OrderByDescending(x => x.DataNotificacao);
        }


        public async Task<IEnumerable<NotificationBase>> ObterTodasNotificacoes(NotificationRequest request)
        {
            if (request.TipoUsuario == Objetos.Enums.TipoUsuario.Club)
            {
                if (request.IdUsuario > 0)
                {
                    var club = await _clubCommand.ObterByIdUsuario(request.IdUsuario);
                    var result = await ObterTodasNotificacoesClube(club.Id);
                    _usuarioContext.SetNotificacoes(result.ToList());

                    return result;
                }
                else
                {
                    return new List<NotificationBase>();
                }
            }
            else
            {
                return new List<NotificationBase>();
            }
        }

        public async Task<SolicitacaoAlunoClub> ObterSolicitacaoAlunoById(long idSolicitacao)
        {
           return await _solicitacaoClubAlunoRepository.ObterSolicitacaoById(idSolicitacao);    
        }

		public async Task<bool> NotificarSolicitacaoAlunoAceito(SolicitacaoAlunoClub solicitacao)
		{
            return await _solicitacaoClubAlunoRepository.NotificarSolicitacaoAlunoAceito(solicitacao);
		}
	}
}
