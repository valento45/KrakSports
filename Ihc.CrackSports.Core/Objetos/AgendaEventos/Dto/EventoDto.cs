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


        public Evento ToEvento()
        {
            return new Evento
            {
                IdEvento = id_evento,
                IdClub1 = id_club1,
                IdClub2 = id_club2,
                DataHoraEvento = data_hora,
                EnderecoResumido = endereco_resumido,
                GolsClub1 = gols_club1,
                GolsClub2 = gols_club2,
                ImagemBase64 = imagem_base64,
                IsEncerrado = is_encerrado,
                Observacoes = observacoes,
                Tipo = (TipoEvento)tipo_evento
            };
        }
    }
}
