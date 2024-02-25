using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Base.Auxiliar;
using Ihc.CrackSports.Core.Objetos.Ranking;
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


        public async Task<RankingExibicao> GetRankingExibicao(Periodo periodo)
        {
            return await _rankingCommand.GetRankingExibicao(periodo);
        }
    }
}
