using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Requests.Clube.Solicitacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Repositorys.Interfaces
{
    public interface ISolicitacaoClubAlunoRepository
    {
        Task<SolicitacaoAlunoClub> EnviarSolicitacao(SolicitacaoAlunoClubRequest solicitacao);        
        Task<bool> AceitarSolicitacao(SolicitacaoAlunoClub solicitacao);
        Task<bool> RemoverSolicitacao(long idSolicitacao);
        Task<bool> PossuiSolicitacaoPendente(long idAluno);
        Task<IEnumerable<SolicitacaoAlunoClub>> ObterTodasSolicitacoesDoClube(long idClube);
        Task<SolicitacaoAlunoClub> ObterSolicitacaoById(long idSolicitacao);
    }
}
