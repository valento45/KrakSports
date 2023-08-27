using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Objetos.Clube;

namespace Ihc.CrackSports.WebApp.Models.Clube
{
    public class ClubViewModel
    {
        public Club DadosClub { get; set; }
        public IFormFile File { get; set; }
        public Usuario DadosUsuario { get; set; }
        public bool isInsert() => DadosClub?.Id <= 0;


        public ClubViewModel()
        {

        }

        public ClubViewModel(string nomeClub, string emailClub)
        {
            if (DadosClub == null)            
                DadosClub = new Club();

            if(DadosUsuario == null)
                DadosUsuario = new Usuario();


            DadosClub.Nome = nomeClub;
            DadosUsuario.Email = emailClub;
        }

        public void InformarClub(Club club)
        {
            if (club != null)
                DadosClub = club;
        }
    }
}
