using Ihc.CrackSports.Core.Objetos.Base.Auxiliar;
using Ihc.CrackSports.Core.Objetos.Competicoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Responses.AgendaEventos
{
    public class EventosResponse
    {

        public IEnumerable<Evento> Eventos { get; private set; }
        public string? Erro { get; set; }
        public Periodo Periodo { get; private set; }

        public EventosResponse()
        {

        }

        public void InformarEventos(IEnumerable<Evento> eventos)
        {
            Eventos = eventos;
        }

        public void InformarPeriodo( DateTime de, DateTime ate)
        {
            Periodo = new Periodo(de, ate);
        }


    }
}
