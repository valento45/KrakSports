using Ihc.CrackSports.Core.Objetos.Competicoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Repositorys.Interfaces;

public interface IPlacarRepository
{
    Task<IEnumerable<Evento>> PlacarEventos(DateTime dataInicio, DateTime dataFim);
}
