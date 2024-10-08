﻿using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.AgendaEventos;
using Ihc.CrackSports.Core.Objetos.Competicoes;
using Ihc.CrackSports.Core.Objetos.Notifications.Base;
using Ihc.CrackSports.Core.Responses.AgendaEventos;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.WebApp.Application.Interfaces;

namespace Ihc.CrackSports.WebApp.Application
{
    public class EventoApplication : IEventoApplication
    {
        private readonly IEventoService _eventoService;
        private readonly IClubService _clubService;
        private readonly INotificationCommand _notificationCommand;
        private readonly IPlacarCommand _placarCommand;

        public EventoApplication(IEventoService eventoService, IClubService clubService,
            INotificationCommand notificationCommand, IPlacarCommand placarCommand)
        {
            _eventoService = eventoService;
            _clubService = clubService;
            _notificationCommand = notificationCommand;
            _placarCommand = placarCommand;
        }


        private async Task<IEnumerable<Evento>> ObterEventosComClubes(IEnumerable<Evento> eventos)
        {
            List<Evento> EventosNew = new List<Evento>();

            foreach (var item in eventos)
            {
                item.InformarClube(await _clubService.ObterById(item.IdClub1));
                item.InformarClube2(await _clubService.ObterById(item.IdClub2));

                EventosNew.Add(item);
            }

            return EventosNew;
        }

        public async Task<bool> EncerrarEvento(long idEvento)
        {
            return await _eventoService.EncerrarEvento(idEvento);
        }

        public async Task<bool> ExcluirEvento(long IdEvento)
        {
            return await _eventoService.ExcluirEvento(IdEvento);
        }

        public async Task<Evento> GetEventoById(long idEvento)
        {
            var evento = await _eventoService.GetEventoById(idEvento);

            evento.InformarClube(await _clubService.ObterById(evento.IdClub1) ?? new Core.Objetos.Clube.Club());
            evento.InformarClube2(await _clubService.ObterById(evento.IdClub2) ?? new Core.Objetos.Clube.Club());

            return evento;
        }

        public async Task<EventosResponse> GetEventos(DateTime dataInicio, DateTime dataFim)
        {
            var eventos = await _eventoService.GetEventos(dataInicio, dataFim);

            var evtOrdered = await this.ObterEventosComClubes(eventos.Eventos);

            eventos.InformarEventos(evtOrdered.OrderByDescending(x => x.Data));



            return eventos;
        }

        public async Task<EventosResponse> GetEventosByIdClube(long IdClube)
        {
            return await _eventoService.GetEventosByIdClube(IdClube);
        }



        public async Task<bool> Salvar(Evento evento)
        {
            if (await _eventoService.Salvar(evento))
            {
                await _notificationCommand.IncluirNotificacao(evento.ObterNotificacao(evento.Clube1));
                await _notificationCommand.IncluirNotificacao(evento.ObterNotificacao(evento.Clube2));

                return true;
            }

            return false;
        }


        public async Task<bool> ExcluirLancamentoPlacar(long idLancamento)
        {
            return await _eventoService.ExcluirLancamentoPlacar(idLancamento);
        }

        public async Task<bool> LancarPlacarEvento(AtletaEvento atletaEvento, bool isEncerrado = false)
        {
            return await _placarCommand.LancarPlacarEvento(atletaEvento, isEncerrado);
        }

        public async Task<IEnumerable<AtletaEvento>> ObterPlacar(long idEvento)
        {
            return await _placarCommand.ObterPlacar(idEvento);
        }
    }
}
