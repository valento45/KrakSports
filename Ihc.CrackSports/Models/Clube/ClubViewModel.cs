using Ihc.CrackSports.Core.Objetos.Clube;

namespace Ihc.CrackSports.WebApp.Models.Clube
{
    public class ClubViewModel
    {
        public Club DadosClub { get; set; }
        public IFormFile File { get; set; }

        public bool isInsert() => DadosClub?.Id <= 0;

        public void InformarClub(Club club)
        {
            if (club != null)
                DadosClub = club;
        }
    }
}
