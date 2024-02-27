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

        public async Task<IEnumerable<Evento>> PlacarEventos(DateTime dataInicio, DateTime dataFim)
        {
            string query = $"select * from sys.agenda_evento_tb ";



            var result = await QueryAsync<EventoDto>(query);

            return result?.Select(x => x.ToEvento())?.OrderBy(x => x.Data)?.AsEnumerable() ?? new List<Evento>();
        }
    }
}
