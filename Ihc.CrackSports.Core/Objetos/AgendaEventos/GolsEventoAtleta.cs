using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.AgendaEventos
{
    public class GolsEventoAtleta
    {
        public long IdEvento { get; set; }
        public long IdAluno { get; set; }
        public long IdClube { get; set; }
        public int GolsMarcados { get; set; }
    }
}
