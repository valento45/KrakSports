using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Competicoes;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Commands;

public class PlacarCommand : IPlacarCommand
{
    private readonly IPlacarRepository placarRepository;

    public async Task<IEnumerable<Evento>> ObterPlacarGeral(DateTime dataInicio, DateTime dataFim)
    {
        return await placarRepository.PlacarEventos(dataInicio, dataFim);
    }
    public PlacarCommand(IPlacarRepository placarRepository)
    {
        this.placarRepository = placarRepository;
    }
}
