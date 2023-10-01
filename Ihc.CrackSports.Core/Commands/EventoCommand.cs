using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.AgendaEventos;
using Ihc.CrackSports.Core.Objetos.Competicoes;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Commands
{
    public class EventoCommand : IEventoCommand
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoCommand(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public async Task<bool> IncluirEvento(Evento evento)
        {
          return await _eventoRepository.IncluirEvento(evento);
        }


        public async Task<bool> AtualizarEvento(Evento evento)
        {
            return await _eventoRepository.AtualizarEvento(evento);
        }

        public async Task<bool> EncerrarEvento(long idEvento)
        {
            return await _eventoRepository.EncerrarEvento(idEvento);
        }

        public async Task<bool> ExcluirEvento(long IdEvento)
        {
            return await _eventoRepository.ExcluirEvento(IdEvento);
        }

        public async Task<Evento> GetEventoById(long IdEvento)
        {
            return await _eventoRepository.GetEventoById(IdEvento); ;
        }

        public async Task<IEnumerable<Evento>> GetEventos(DateTime dataInicio, DateTime dataFim)
        {
            return await _eventoRepository.GetEventos(dataInicio, dataFim);
        }

        public async Task<IEnumerable<Evento>> GetEventosByIdClube(long IdClube)
        {
            return await _eventoRepository.GetEventosByIdClube(IdClube);
        }

      
        public async Task<bool> LancarPlacarEvento(List<GolsEventoAtleta> golsMarcados, bool isEncerrado = false)
        {
            return await _eventoRepository.LancarPlacarEvento(golsMarcados, isEncerrado);
        }
    }
}
