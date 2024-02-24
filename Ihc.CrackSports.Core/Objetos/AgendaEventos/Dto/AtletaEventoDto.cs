using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.AgendaEventos.Dto
{
    public class AtletaEventoDto
    {
        public long id_evento { get; set; }
        public long id_aluno{ get; set; }
        public long id_clube { get; set; }
        public int gols_marcados { get; set; }

        public AtletaEventoDto()
        {
            
        }


        public AtletaEvento ToObjeto()
        {
            return new AtletaEvento
            {
                IdEvento = id_evento,
                IdAluno = id_aluno,
                IdClube = id_clube, 
                GolsMarcados = gols_marcados
            };
        }

    }
}