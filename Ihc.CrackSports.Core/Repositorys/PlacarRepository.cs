using Ihc.CrackSports.Core.Objetos.AgendaEventos;
using Ihc.CrackSports.Core.Objetos.AgendaEventos.Dto;
using Ihc.CrackSports.Core.Objetos.Competicoes;
using Ihc.CrackSports.Core.Repositorys.Base;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Repositorys
{
    public class PlacarRepository : RepositoryBase, IPlacarRepository
    {
        public PlacarRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task<bool> LancarPlacarEvento(AtletaEvento atletaEvento, bool isEncerrado = false)
        {

            string query = "INSERT INTO sys.atleta_evento_tb (id_evento, id_aluno, id_clube, gols_marcados)" +
                $" values ({atletaEvento.IdEvento}, {atletaEvento.IdAluno}, {atletaEvento.IdClube}, {atletaEvento.GolsMarcados});";

            var result = await base.ExecuteAsync(query);

            return result;
        }

        public async Task<IEnumerable<AtletaEvento>> ObterPlacarById(long idEvento)
        {
            string query = $"select * from sys.atleta_evento_tb where id_evento = {idEvento}";

            var result = await base.QueryAsync<AtletaEventoDto>(query);

            return result?.Select(x => x.ToObjeto()) ?? new List<AtletaEvento>();
        }
    }
}
