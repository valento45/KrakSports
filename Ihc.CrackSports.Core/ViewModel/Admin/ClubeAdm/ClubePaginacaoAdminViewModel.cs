using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.ViewModel.Admin.ClubeAdm
{
    public class ClubePaginacaoAdminViewModel
    {

        public PaginacaoBaseViewModel<Club> PaginacaoClube{ get; set; }



        public ClubePaginacaoAdminViewModel()
        {
            PaginacaoClube = new PaginacaoBaseViewModel<Club>();
        }

        public ClubePaginacaoAdminViewModel(PaginacaoBaseViewModel<Club> paginacaoClube)
        {
            PaginacaoClube = paginacaoClube;
        }

        public ClubePaginacaoAdminViewModel(PaginacaoBaseViewModelRequest<Club> request)
        {
            PaginacaoClube = new PaginacaoBaseViewModel<Club>(request);
        }


    }
}
