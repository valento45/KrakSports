using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Objetos.Clube.Dto;
using Ihc.CrackSports.Core.Objetos.Enums;
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
		private readonly IAlunoRepository alunoRepository;

		public SolicitacaoAlunoClubRepository(IDbConnection connection, IAlunoRepository alunoRepository) : base(connection)
		{
			this.alunoRepository = alunoRepository;
		}

		public async Task<bool> AceitarSolicitacao(SolicitacaoAlunoClub solicitacao)
		{
			bool sucesso = false;
			string query = $"update sys.aluno_tb set id_club = {solicitacao.IdClub} where id_aluno = {solicitacao.IdAluno}";
			sucesso = await base.ExecuteAsync(query);

			query = $"update sys.solicitacao_aluno_club_tb set is_aceito = true where id_aluno = {solicitacao.IdAluno} AND id_club = {solicitacao.IdClub}";
			sucesso = await base.ExecuteAsync(query);

			return sucesso;
		}

		public async Task<SolicitacaoAlunoClub> EnviarSolicitacao(SolicitacaoAlunoClubRequest solicitacao)
		{
			string query = "insert into sys.solicitacao_aluno_club_tb (id_aluno, id_club, data_solicitacao, is_aceito)" +
				 " values (@id_aluno, @id_club, @data_solicitacao, @is_aceito) returning id_solicitacao";

			NpgsqlCommand cmd = new NpgsqlCommand(query);
			cmd.Parameters.AddWithValue(@"id_aluno", solicitacao.Solicitacao.IdAluno);
			cmd.Parameters.AddWithValue(@"id_club", solicitacao.Solicitacao.IdClub);
			cmd.Parameters.AddWithValue(@"data_solicitacao", solicitacao.Solicitacao.DataNotificacao);
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

		public async Task<bool> RemoverSolicitacao(long idSolicitacao)
		{
			string query = "delete from sys.solicitacao_aluno_club_tb where id_solicitacao = " + idSolicitacao;
			return await base.ExecuteAsync(query);
		}

		public async Task<bool> PossuiSolicitacaoPendente(long idAluno)
		{
			string query = "select count(*) from sys.solicitacao_aluno_club_tb where id_aluno = " + idAluno;

			var result = await base.QueryAsync(query);

			return result.FirstOrDefault() > 0;
		}

		public async Task<IEnumerable<SolicitacaoAlunoClub>> ObterTodasSolicitacoesDoClube(long idClube)
		{
			string query = $"select * from sys.solicitacao_aluno_club_tb where id_club = {idClube} and is_aceito = false order by data_solicitacao desc LIMIT 50";
			var result = await base.QueryAsync<SolicitacaoAlunoDto>(query);

			return result.Select(x => x.ToSolicitacao());
		}

		public async Task<SolicitacaoAlunoClub> ObterSolicitacaoById(long idSolicitacao)
		{
			string query = $"select * from sys.solicitacao_aluno_club_tb where id_solicitacao = {idSolicitacao} LIMIT 1";
			var result = await base.QueryAsync<SolicitacaoAlunoDto>(query);

			return result.FirstOrDefault().ToSolicitacao();
		}

		public async Task<bool> NotificarSolicitacaoAlunoAceito(SolicitacaoAlunoClub solicitacao)
		{
			string query = "insert into sys.notificacao_tb (id_aluno, id_club, data_notificacao, is_visto, tipo_usuario, notificacao, imagem_notificacao," +
				" link_redirect) values (@id_aluno, @id_club, @data_notificacao, @is_visto, @tipo_usuario, @notificacao, @imagem_notificacao, @link_redirect)" +
				" returning id_notificacao";

			NpgsqlCommand cmd = new NpgsqlCommand(query);
			cmd.Parameters.AddWithValue(@"id_aluno", solicitacao.IdAluno);
			cmd.Parameters.AddWithValue(@"id_club", solicitacao.IdClub);
			cmd.Parameters.AddWithValue(@"data_notificacao", solicitacao?.DataNotificacao ?? DateTime.Now);
			cmd.Parameters.AddWithValue(@"is_visto", solicitacao?.IsVisto ?? false);
			cmd.Parameters.AddWithValue(@"tipo_usuario", (int)TipoUsuario.Aluno);
			cmd.Parameters.AddWithValue(@"notificacao", solicitacao?.Notificacao ?? "");
			cmd.Parameters.AddWithValue(@"imagem_notificacao", solicitacao.ImagemNotificacao ?? "");
			cmd.Parameters.AddWithValue(@"link_redirect", solicitacao?.LinkRedirect ?? "");

			int codigo;
			var result = await base.ExecuteScalarAsync(cmd);
			int.TryParse(result?.ToString(), out codigo);

			return codigo > 0;
		}
	}
}
