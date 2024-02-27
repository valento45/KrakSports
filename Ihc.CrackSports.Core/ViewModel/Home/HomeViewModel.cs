using Ihc.CrackSports.Core.Objetos.Colaborador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.ViewModel.Home
{
    public class HomeViewModel
    {

        public IEnumerable<Patrocinador> Patrocinadores { get; private set; }



        public HomeViewModel()
        {
            Patrocinadores = new List<Patrocinador>();
        }


        public void InformarPatrocinadores(IEnumerable<Patrocinador> patros)
        {
            Patrocinadores = patros;    
        }

    }
}
