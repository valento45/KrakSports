using Ihc.CrackSports.Core.Objetos.Alunos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Ranking
{
    public class RankingAtleta
    {

        public long IdAluno { get; set; }
        public string Nome { get; set; }
        public string NomeClube { get; set; }
        public int TotalGols { get; set; }
        public DateTime UltimoJogo { get; set; }
        public int PosicaoRanking { get; set; }
        public string ImagemBase64 { get; set; }



        public RankingAtleta()
        {
            
        }

        public RankingAtleta(DataRow dr)
        {
            
        }
    }
}
