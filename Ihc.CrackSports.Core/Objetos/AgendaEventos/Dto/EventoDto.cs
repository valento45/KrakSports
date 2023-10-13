using Ihc.CrackSports.Core.Objetos.Competicoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.AgendaEventos.Dto
{
    public class EventoDto
    {
        public long id_evento { get; set; }
        public DateTime data_hora { get; set; }
        public int tipo_evento { get; set; }
        public long id_club1 { get; set; }
        public long id_club2 { get; set; }
        public string endereco_resumido { get; set; }
        public string imagem_base64 { get; set; }
        public string observacoes { get; set; }

            
        public int gols_club1 { get; set; }
        public int gols_club2 { get; set; }
        public bool is_encerrado { get; set; }
        public string hora_evento { get; set; }


        public Evento ToEvento()
        {
            return Evento.ConvertToEvento(this);
        }
    }
}
