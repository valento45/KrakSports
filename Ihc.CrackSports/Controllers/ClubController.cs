using Ihc.CrackSports.Core.Authorization.Claims;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.WebApp.Models.Clube;
using Microsoft.AspNetCore.Mvc;

namespace Ihc.CrackSports.WebApp.Controllers
{
    public class ClubController : ControllerBase
    {
        private readonly IClubService _clubService;

        public ClubController(IClubService clubService) : base(clubService)
        {
            _clubService = clubService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var obj = await _clubService.ObterByNome(string.Empty, 0);

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Cadastro(long? idClub)
        {
            var result = new ClubViewModel();

            if (idClub == null || idClub <= 0)
                return View(result);

            var club = await _clubService.ObterById(idClub.Value);

            if (club != null)
            {
                if (club.Id == GetIdUsuarioLogado() && base.CanAccess(User, Roles.CLUB))
                    return View(result);

                else if (base.CanAccess(User, Roles.ADMINISTRADOR))
                    return View(result);

                else
                    return View("Unauthorized");
            }

            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> Cadastro(ClubViewModel model)
        {
            if (model != null)
            {
                if (model.isInsert())
                {
                    if (!base.CanInsert(User, Roles.CLUB))
                        return View("Unauthorized");
                }
                else
                    if (!base.CanUpdate(User, Roles.CLUB))
                    return View("Unauthorized");


                var result = await _clubService.Salvar(model.DadosClub);

                if (result.IsSuccessStatusCode)
                    return View(model);
                else
                    throw new Exception($"Erro ao salvar dados do club. Codigo {result.StatusCode} - {result.Message}");

            }

            return View(model);
        }
    }
}
