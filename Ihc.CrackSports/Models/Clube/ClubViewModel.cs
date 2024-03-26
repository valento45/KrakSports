using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.WebApp.Models.Alunos;

namespace Ihc.CrackSports.WebApp.Models.Clube
{
    public class ClubViewModel
    {
        public Club DadosClub { get; set; }
        public IFormFile File { get; set; }
        public Usuario DadosUsuario { get; set; }
        public bool isInsert() => DadosClub?.Id <= 0;
        public PaginacaoAlunoViewModel Atletas { get; set; }


        public string Message { get; set; }

        public ClubViewModel()
        {
            
            DadosClub = new Club();
            DadosUsuario = new Usuario();
            Atletas = new PaginacaoAlunoViewModel();
        }

        public ClubViewModel(string nomeClub, string emailClub) : base()
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
