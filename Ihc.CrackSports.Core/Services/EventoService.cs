using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.AgendaEventos;
using Ihc.CrackSports.Core.Objetos.Competicoes;
using Ihc.CrackSports.Core.Responses.AgendaEventos;
using Ihc.CrackSports.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Services
{
    public class EventoService : IEventoService
    {
        private readonly IEventoCommand _eventoCommand;

        public EventoService(IEventoCommand eventoCommand)
        {
            _eventoCommand  = eventoCommand;
        }

        public async Task<bool> IncluirEvento(Evento evento)
        {
            return await _eventoCommand.IncluirEvento(evento);
        }

        public async Task<bool> AtualizarEvento(Evento evento)
        {
            return await _eventoCommand.AtualizarEvento(evento);
        }

        public async Task<bool> EncerrarEvento(long idEvento)
        {
            return await _eventoCommand.EncerrarEvento(idEvento);
        }

        public async Task<bool> ExcluirEvento(long IdEvento)
        {
            return await _eventoCommand.ExcluirEvento(IdEvento);
        }

        public async Task<Evento> GetEventoById(long IdEvento)
        {
            return await _eventoCommand.GetEventoById(IdEvento);
        }

        public async Task<EventosResponse> GetEventos(DateTime dataInicio, DateTime dataFim)
        {
            EventosResponse response = new EventosResponse();

            response.InformarEventos(await _eventoCommand.GetEventos(dataInicio, dataFim));

            return response;
        }

        public async Task<EventosResponse> GetEventosByIdClube(long IdClube)
        {
            EventosResponse response = new EventosResponse();

            response.InformarEventos(await _eventoCommand.GetEventosByIdClube(IdClube));

            return response;
        }

       

        public async Task<bool> LancarPlacarEvento(List<GolsEventoAtleta> golsMarcados, bool isEncerrado = false)
        {
            return await _eventoCommand.LancarPlacarEvento(golsMarcados, isEncerrado);
        }
    }
}
