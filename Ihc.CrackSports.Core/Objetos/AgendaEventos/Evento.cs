using Ihc.CrackSports.Core.Objetos.AgendaEventos;
using Ihc.CrackSports.Core.Objetos.Base.Pessoas;
using Ihc.CrackSports.Core.Objetos.Clube;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Competicoes
{
    public class Evento
    {
        public long IdEvento { get; set; }
        public DateTime DataHoraEvento { get; set; }
        public TipoEvento   Tipo { get; set; }
        public long IdClub1 { get; set; }
        public long IdClub2 { get; set; }
        public string EnderecoResumido { get; set; }
        public string ImagemBase64 { get; set; }
        public string Observacoes { get; set; }



        public Evento()
        {
            
        }

        public Evento(DataRow dr)
        {
            
        }
    }
}
