using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Requests.Agenda
{
    public class ListarEventosRequest
    {
        public int Mes { get; set; }
        public int Ano { get; set; }


        public bool IsValido()
        {
            return Mes > 0 && Ano > 0;
        }
    }
}
