using Ihc.CrackSports.Core.Objetos.Competicoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Commands.Interfaces
{
    public interface IPlacarCommand

    {
        Task<IEnumerable<Evento>> ObterPlacarGeral(DateTime dataInicio, DateTime dataFim);
        
    }
}
