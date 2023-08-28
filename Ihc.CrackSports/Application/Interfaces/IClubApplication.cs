using Ihc.CrackSports.WebApp.Models.Clube;
using Ihc.CrackSports.WebApp.Models.Usuarios;

namespace Ihc.CrackSports.WebApp.Application.Interfaces
{
    public interface IClubApplication
    {
        Task<ClubViewModel> GetClubViewModel(long idUsuario);
    }
}
