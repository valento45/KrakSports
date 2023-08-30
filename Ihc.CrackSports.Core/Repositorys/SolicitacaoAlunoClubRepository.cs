using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Repositorys.Base;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using Ihc.CrackSports.Core.Requests.Clube.Solicitacoes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Repositorys
{
    public class SolicitacaoAlunoClubRepository : RepositoryBase, ISolicitacaoClubAlunoRepository
    {
        public SolicitacaoAlunoClubRepository(IDbConnection connection) : base(connection)
        {

        }

        public Task<bool> AceitarSolicitacao(SolicitacaoAlunoClub solicitacao)
        {
            throw new NotImplementedException();
        }

        public Task<SolicitacaoAlunoClub> EnviarSolicitacao(SolicitacaoAlunoClubRequest solicitacao)
        {
            throw new NotImplementedException();
        }        

        public Task<bool> RemoverSolicitacao(long idSolicitacao)
        {
            throw new NotImplementedException();
        }
        public Task<bool> PossuiSolicitacaoPendente(long idAluno)
        {
            throw new NotImplementedException();
        }
    }
}
