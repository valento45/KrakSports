using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Base.Auxiliar
{
    public class Periodo
    {
        public DateTime De { get; set; }
        public DateTime Ate { get; set; }

        public Periodo()
        {
            De = new DateTime();
            Ate = new DateTime();
        }

        public Periodo(DateTime de, DateTime ate)
        {
            De = de;
            Ate = ate;
        }

        public bool IsValido()
        {
            return De > new DateTime() && Ate > new DateTime();
        }
    }
}
