using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Ranking
{
    public class RankingExibicao
    {

        public string Periodo { get; set; }

        public ICollection<RankingAtleta> AtletasRanking { get; set; }


        public RankingExibicao()
        {
            AtletasRanking = new List<RankingAtleta>();
        }


        public void AddRankingAtleta(RankingAtleta rankingAtleta)
        {
            if (AtletasRanking == null)
                AtletasRanking = new List<RankingAtleta>();

            AtletasRanking.Add(rankingAtleta);
        }
    }
}
