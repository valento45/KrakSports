using Ihc.CrackSports.Core.Objetos.Alunos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Clube.Dto
{
    public class SolicitacaoAlunoDto
    {
        public long id_solicitacao { get; set; }
        public long id_aluno { get; set; }
		public long id_club { get; set; }
        public DateTime data_solicitacao { get; set; }
        public bool is_aceito { get; set; }
        public bool is_visto { get; set; }

        public SolicitacaoAlunoClub ToSolicitacao()
        {
            return new SolicitacaoAlunoClub
            {
                IdSolicitacao = id_solicitacao,
                IdNotificacao = id_solicitacao,
                IdAluno = id_aluno,
                IdClub = id_club,
                DataNotificacao = data_solicitacao,
                IsAceito = is_aceito,
                IsVisto = is_visto
            };
        }
    }
}
