using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Clube
{
    public class SolicitacaoAlunoClub
    {
        public long IdSolicitacao { get; set; }
        public long IdAluno { get; set; }
        public long IdClub { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public bool IsAceito { get; set; }

        public SolicitacaoAlunoClub()
        {
            
        }
    }
}
