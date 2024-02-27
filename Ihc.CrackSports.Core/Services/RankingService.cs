using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Base.Auxiliar;
using Ihc.CrackSports.Core.Objetos.Ranking;
using Ihc.CrackSports.Core.Objetos.Ranking.Enums;
using Ihc.CrackSports.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Services
{
    public class RankingService : IRankingService
    {
        private readonly IRankingCommand _rankingCommand;

        public RankingService(IRankingCommand rankingCommand)
        {
            _rankingCommand = rankingCommand;
        }


        public async Task<RankingExibicao> GetRankingExibicao(PeriodoRanking periodoRanking)
        {
            Periodo periodo = null;

            if (periodoRanking == PeriodoRanking.Semanal)
            {
                DateTime de = DateTime.Now.AddDays(-8);
                DateTime ate = DateTime.Now;


                periodo = new Periodo(de, ate);
            }
            else if (periodoRanking == PeriodoRanking.Mensal)
            {
                DateTime de = DateTime.Parse($"01/{DateTime.Now.Month}/{DateTime.Now.Year}");
                DateTime ate = de.AddMonths(1).AddDays(-1);


                periodo = new Periodo(de, ate);
            }
            else if (periodoRanking == PeriodoRanking.Anual)
            {
                DateTime de = DateTime.Parse($"01/01/{DateTime.Now.Year}");
                DateTime ate = DateTime.Parse($"31/12/{DateTime.Now.Year}");


                periodo = new Periodo(de, ate);
            }
            else
            {
                DateTime de = DateTime.Parse($"01/01/2023");
                DateTime ate = DateTime.Parse($"01/01/2025");


                periodo = new Periodo(de, ate);
            }




            return await _rankingCommand.GetRankingExibicao(periodo ?? new Periodo());
        }
    }
}
