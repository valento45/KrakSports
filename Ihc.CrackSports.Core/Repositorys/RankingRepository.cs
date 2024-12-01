using Ihc.CrackSports.Core.Objetos.Base.Auxiliar;
using Ihc.CrackSports.Core.Objetos.Ranking;
using Ihc.CrackSports.Core.Objetos.Ranking.Dto;
using Ihc.CrackSports.Core.Repositorys.Base;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Repositorys
{
    public class RankingRepository : RepositoryBase, IRankingRepository
    {
        public RankingRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }



        public async Task<List<RankingAtleta>> GetRankingExibicao(Periodo periodo)
        {
            string query = @"select  atl.id_aluno, atl.nome, cl.nome_fantasia as nome_clube, atl.foto_base64, sum(aevt.gols_marcados) as gols_marcados from sys.agenda_evento_tb as evt
inner join sys.atleta_evento_tb as  aevt ON evt.id_evento = aevt.id_evento
inner join sys.aluno_tb as atl ON atl.id_aluno = aevt.id_aluno
inner join sys.club_tb as cl on cl.id_club = atl.id_club";

            query += $" where evt.data_hora between to_timestamp('{periodo.De.ToString("dd-MM-yyyy")}', 'dd-MM-yyyy') AND to_timestamp('{periodo.Ate.ToString("dd-MM-yyyy")}','dd-MM-yyyy')";
            query += @" group by atl.id_aluno, atl.nome, cl.nome_fantasia order by gols_marcados desc limit 3";


            var result = await base.QueryAsync<RankingAtletaDto>(query) ?? new List<RankingAtletaDto>();

            return result.Select(x => x.ToObjeto()).ToList();
        }
    }
}
