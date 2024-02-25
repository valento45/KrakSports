using Ihc.CrackSports.Core.Objetos.AgendaEventos;
using Ihc.CrackSports.Core.Objetos.Competicoes;
using Ihc.CrackSports.Core.Responses.AgendaEventos;

namespace Ihc.CrackSports.WebApp.Application.Interfaces
{
    public interface IEventoApplication
    {
        Task<Evento> GetEventoById(long idEvento);

        Task<bool> LancarPlacarEvento(AtletaEvento atletaEvento, bool isEncerrado = false);


        Task<bool> EncerrarEvento(long idEvento);
        
        Task<EventosResponse> GetEventos(DateTime dataInicio, DateTime dataFim);
       
        Task<EventosResponse> GetEventosByIdClube(long IdClube);

        Task<bool> Salvar(Evento evento);

        Task<bool> ExcluirEvento(long IdEvento);

        Task<IEnumerable<AtletaEvento>> ObterPlacar(long idEvento);

        Task<bool> ExcluirLancamentoPlacar(long idLancamento);
    }
}
