using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Base.Auxiliar;
using Ihc.CrackSports.Core.Objetos.Ranking;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Commands
{
    public class RankingCommand : IRankingCommand
    {
        private readonly IRankingRepository _rankingRepository;
        private readonly IAlunoRepository _alunoRepository;

        public RankingCommand(IRankingRepository rankingRepository, IAlunoRepository alunoRepository)
        {
            _rankingRepository = rankingRepository;
            _alunoRepository = alunoRepository;
        }



        public async Task<RankingExibicao> GetRankingExibicao(Periodo periodo)
        {
            var result = new RankingExibicao();

            var atletasEvento = await _rankingRepository.GetRankingExibicao(periodo);

            int posicao = 1;
            foreach(var obj  in atletasEvento)
            {
                obj.PosicaoRanking = posicao;                

                result.AddRankingAtleta(obj);

                posicao++;
            }


            return result;
        }
    }
}
