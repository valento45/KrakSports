using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Competicoes;

namespace Ihc.CrackSports.WebApp.Models.Eventos
{
    public class AtletaEventoGolsViewModel
    {
        public Evento Evento{ get; set; }
        public Aluno Aluno { get; set; }
        public int Gols { get; set; }

        public AtletaEventoGolsViewModel()
        {
                
        }
    }
}
