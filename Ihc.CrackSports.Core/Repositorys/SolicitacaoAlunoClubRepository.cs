using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Repositorys.Base;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using Ihc.CrackSports.Core.Requests.Clube.Solicitacoes;
using Npgsql;
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

        public async Task<SolicitacaoAlunoClub> EnviarSolicitacao(SolicitacaoAlunoClubRequest solicitacao)
        {
            string query = "insert into sys.solicitacao_aluno_club_tb (id_aluno, id_club, data_solicitacao, is_aceito)" +
                 " values (@id_aluno, @id_club, @data_solicitacao, @is_aceito) returning id_solicitacao";

            NpgsqlCommand cmd = new NpgsqlCommand(query);
            cmd.Parameters.AddWithValue(@"id_aluno", solicitacao.Solicitacao.IdAluno);
            cmd.Parameters.AddWithValue(@"id_club", solicitacao.Solicitacao.IdClub);
            cmd.Parameters.AddWithValue(@"data_solicitacao", solicitacao.Solicitacao.DataSolicitacao);
            cmd.Parameters.AddWithValue(@"is_aceito", solicitacao.Solicitacao.IsAceito);



            var result = await base.ExecuteScalarAsync(cmd);

            if (result != null)
            {
                long id;
                long.TryParse(result.ToString(), out id);
                solicitacao.Solicitacao.IdSolicitacao = id;
                return solicitacao.Solicitacao;
            }
            else
                throw new Exception("Erro ao enviar solicitação ao club." + nameof(SolicitacaoAlunoClubRepository));
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
