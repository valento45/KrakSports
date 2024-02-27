using Ihc.CrackSports.Core.Objetos.AgendaEventos;
using Ihc.CrackSports.Core.Objetos.Competicoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Repositorys.Interfaces;

public interface IPlacarRepository
{
    Task<IEnumerable<AtletaEvento>> ObterPlacarById(long idEvento);
    Task<bool> LancarPlacarEvento(AtletaEvento atletaEvento, bool isEncerrado = false);
}
