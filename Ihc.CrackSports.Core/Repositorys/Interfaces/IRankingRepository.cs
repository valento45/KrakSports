using Ihc.CrackSports.Core.Objetos.Base.Auxiliar;
using Ihc.CrackSports.Core.Objetos.Ranking;
using Ihc.CrackSports.Core.Objetos.Ranking.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Repositorys.Interfaces
{
    public interface IRankingRepository
    {

        Task<List<RankingAtleta>> GetRankingExibicao(Periodo periodo);
    }
}
