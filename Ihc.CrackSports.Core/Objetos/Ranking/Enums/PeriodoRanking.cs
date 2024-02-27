using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Ranking.Enums
{
    public enum PeriodoRanking : int
    {
        [Description("Default")]
        Default = 0,

        [Description("Semanal")]
        Semanal = 1,

        [Description("Mensal")]
        Mensal = 2,

        [Description("Anual")]
        Anual = 3
    }
}
