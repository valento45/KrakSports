using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.WebApp.Application.Interfaces;
using Ihc.CrackSports.WebApp.Models.Usuarios;

namespace Ihc.CrackSports.WebApp.Application
{
    public class ClubApplication : IClubApplication
    {
        private readonly IClubService _clubService;

        public ClubApplication(IClubService clubService)
        {
            _clubService = clubService;
        }


        public async Task<MinhaContaViewModel> GetClubViewModel(long idUsuario)
        {
            var result = new MinhaContaViewModel();

            result.InformarClub(await _clubService.ObterByIdUsuario(idUsuario));
            return result;
        }
    }
}
