using Ihc.CrackSports.WebApp.Models.Usuarios;

namespace Ihc.CrackSports.WebApp.Application.Interfaces
{
    public interface IAlunoApplication
    {
        Task<MinhaContaViewModel> GetAlunoViewModel(long idUsuario);
    }
}
