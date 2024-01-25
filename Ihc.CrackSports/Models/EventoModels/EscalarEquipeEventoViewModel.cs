using Ihc.CrackSports.Core.Objetos.AgendaEventos;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Objetos.Competicoes;

namespace Ihc.CrackSports.WebApp.Models.EventoModels
{
    public class EscalarEquipeEventoViewModel
    {
        public Club Clube{ get; private set; }
        public Evento EventoFutebol { get; private set; }

        public IEnumerable<AtletaEvento> JogadoresEscalados { get; private set; }


        public EscalarEquipeEventoViewModel()
        {
            Clube = new Club();
            EventoFutebol = new Evento();
        }

        public void InformarClube(Club club)
        {
            Clube = club;
        }

        public void InformarEvento(Evento evento)
        {
            EventoFutebol = evento;
        }

        public void InformarJogadoresEscalados(IEnumerable<AtletaEvento> atletasEvento)
        {
            JogadoresEscalados = atletasEvento;
        }
    }
}
