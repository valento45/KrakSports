using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Clube;

namespace Ihc.CrackSports.WebApp.Models.Usuarios
{
    public class MinhaContaViewModel
    {
        public Aluno DadosAluno { get; set; }
        public Usuario DadosUsuario { get; set; }
        public Club DadosClub { get; private set; }


        public MinhaContaViewModel()
        {            
            DadosAluno = new Aluno();
            DadosUsuario = new Usuario();
        }

        public MinhaContaViewModel(Aluno aluno, Usuario usuario)
        {
            DadosAluno = aluno;
            DadosUsuario = usuario;
        }

        public void InformarClub(Club club)
        {
            DadosClub = club;
        }
    }
}
