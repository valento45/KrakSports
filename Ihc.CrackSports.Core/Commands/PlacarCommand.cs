using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.AgendaEventos;
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


    public PlacarCommand(IPlacarRepository placarRepository)
    {
        this.placarRepository = placarRepository;
    }

    public async Task<bool> LancarPlacarEvento(AtletaEvento atletaEvento, bool isEncerrado = false)
    {
        return await placarRepository.LancarPlacarEvento(atletaEvento, isEncerrado);
    }

    public async Task<IEnumerable<AtletaEvento>> ObterPlacar(long idEvento)
    {
        return await placarRepository.ObterPlacarById(idEvento);
    }


  
}
