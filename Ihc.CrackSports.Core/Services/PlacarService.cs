using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Competicoes;
using Ihc.CrackSports.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Services
{
    public class PlacarService : IPlacarService
    {
        private readonly IPlacarCommand placarCommand;

        public PlacarService(IPlacarCommand placarCommand)
        {
            this.placarCommand = placarCommand;
        }

        public async Task<IEnumerable<Evento>> ObterPlacarGeral(DateTime dataInicio, DateTime dataFim)
        {
            return await placarCommand.ObterPlacarGeral(dataInicio, dataFim);
        }
        
    }
}
