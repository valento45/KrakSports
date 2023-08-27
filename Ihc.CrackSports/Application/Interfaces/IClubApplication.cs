using Ihc.CrackSports.WebApp.Models.Usuarios;

namespace Ihc.CrackSports.WebApp.Application.Interfaces
{
    public interface IClubApplication
    {
        Task<MinhaContaViewModel> GetClubViewModel(long idUsuario);
    }
}
