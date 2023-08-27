using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Objetos.Enums;

namespace Ihc.CrackSports.WebApp.Models.Usuarios
{
    public class MinhaContaViewModel
    {
        public Aluno DadosAluno { get; set; }
        public Usuario DadosUsuario { get; set; }
        public Club DadosClub { get; private set; }
        public TipoUsuario TipoUsuario { get; set; }


        public MinhaContaViewModel()
        {            
            DadosAluno = new Aluno();
            DadosUsuario = new Usuario();
        }

        public MinhaContaViewModel(Aluno aluno, Usuario usuario)
        {
            DadosAluno = aluno;
            DadosUsuario = usuario;
            TipoUsuario = TipoUsuario.Aluno;

		}

        public MinhaContaViewModel(Club club, Usuario usuario)
        {
            DadosClub = club;
            DadosUsuario = usuario;
            TipoUsuario = TipoUsuario.Club;
        }

		public MinhaContaViewModel(Aluno aluno, Usuario usuario, Club? club, TipoUsuario tipo)
		{
            DadosAluno = aluno;
			DadosClub = club ?? new Club();
			DadosUsuario = usuario;
            TipoUsuario = tipo;
		}

		public void InformarClub(Club club)
        {
            DadosClub = club;
        }
    }
}
