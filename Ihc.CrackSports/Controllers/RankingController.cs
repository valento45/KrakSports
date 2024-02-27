using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Base.Auxiliar;
using Ihc.CrackSports.Core.Objetos.Ranking.Enums;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.WebApp.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ihc.CrackSports.WebApp.Controllers
{
    public class RankingController : ControllerBase
    {
        private readonly IRankingService _rankingService;

        public RankingController(IClubService clubService, IAlunoService alunoService, UserManager<Usuario> userManager, INotificationCommand notificationCommand, IUsuarioContext httpContextAccessor,
            IMessageApplication messageApplication, IRankingService rankingService)
            : base(clubService, alunoService, userManager, notificationCommand, httpContextAccessor, messageApplication)
        {
            _rankingService = rankingService;
        }

        [HttpGet]
        public async Task<IActionResult> RankingAtletas()
        {

            var rankingExibicao = await _rankingService.GetRankingExibicao(PeriodoRanking.Default);

            return View(rankingExibicao);
        }


        [HttpGet]
        public async Task<IActionResult> FiltrarRankingAtletas(PeriodoRanking periodoRanking)
        {
            var rankingExibicao = await _rankingService.GetRankingExibicao(periodoRanking);

            return View(nameof(RankingAtletas), rankingExibicao);
        }
    }
}
