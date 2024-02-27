using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.AgendaEventos;
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

        public async Task<bool> LancarPlacarEvento(AtletaEvento atletaEvento, bool isEncerrado = false)
        {
            return await placarCommand.LancarPlacarEvento(atletaEvento, isEncerrado);
        }

        public async Task<IEnumerable<AtletaEvento>> ObterPlacar(long idEvento)
        {
            return await placarCommand.ObterPlacar(idEvento);
        }
    }
}
