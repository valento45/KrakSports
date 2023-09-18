using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.AgendaEventos
{
    public enum TipoEvento : int
    {
        [Description("Treino")]
        Treino = 0,

        [Description("Competição")]
        Competicao = 1,

        [Description("Torneio")]
        Torneio = 2,

        [Description("Campeonato")]
        Campeonato = 3,

        [Description("Amistoso")]
        Amistoso = 4
    }
}
