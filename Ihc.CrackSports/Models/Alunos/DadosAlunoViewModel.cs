using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Clube;

namespace Ihc.CrackSports.WebApp.Models.Alunos
{
    public class DadosAlunoViewModel
    {
        public Aluno DadosAluno { get; set; }
        public List<Club> Clubs { get; set; }
        public IFormFile File { get; set; }
    }
}
