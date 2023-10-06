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
        public DateTime DataHora { get; set; }
        public TipoEvento Tipo { get; set; }
        public long IdClub1 { get; set; }
        public long IdClub2 { get; set; }
        public string EnderecoResumido { get; set; }
        public string ImagemBase64 { get; set; }
        public string Observacoes { get; set; }
        public int GolsClub1 { get; set; }
        public int GolsClub2 { get; set; }
        public bool IsEncerrado { get; set; }
        public TimeSpan HoraEvento { get; set; }
        public Club Clube1 { get; private set; }
        public Club Clube2 { get; private set; }

        public Evento()
        {
            DataHora = DateTime.Parse($"01/01/{DateTime.Now.Year}") ;
        }

        public Evento(DataRow dr)
        {

        }

        public void InformarClube(Club club)
        {
            Clube1 = club;
        }
        public void InformarClube2(Club club)
        {
            Clube2 = club;
        }


        public bool IsValido() => IdClub1 > 0 && IdClub2 > 0;
    }
}
