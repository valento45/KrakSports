using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Ranking.Dto
{
    public class RankingAtletaDto
    {

        public long id_aluno { get; set; }
        public string nome { get; set; }
        public string nome_clube { get; set; }
        public DateTime data_evento { get; set; }
        public int gols_marcados { get; set; }
        public string foto_base64 { get; set; }


        public RankingAtleta ToObjeto()
        {
            return new RankingAtleta()
            {
                IdAluno = id_aluno,
                Nome = nome,
                TotalGols = gols_marcados,
                NomeClube = nome_clube,
                ImagemBase64 = foto_base64
            };
        }
    }
}
