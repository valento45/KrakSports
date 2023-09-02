using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Utils.Paginacoes;

namespace Ihc.CrackSports.WebApp.Models.Clube
{
    public class PaginacaoClubViewModel
    {
        public bool NextPage { get; set; }
        public bool PreviousPage { get; set; }
        public bool FirstPage { get; set; }
        public bool LastPage { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }

        public Paginacao<Club> ClubsPaginacao { get; set; }
        public List<Club> TodosClubs
        {
            get
            {
                return ClubsPaginacao.ObterListaCompleta().ToList();
            }
        }

        public PaginacaoClubViewModel()
        {

        }

        public PaginacaoClubViewModel(Paginacao<Club> paginacaoClub)
        {
            ClubsPaginacao = paginacaoClub;
            PageNumber = paginacaoClub.PageNumber;
            PageCount = paginacaoClub.PageCount;
        }

        public PaginacaoClubViewModel(PaginacaoClubViewModelRequest request)
        {           
            ClubsPaginacao = new Paginacao<Club>(request.TodosClubs.AsQueryable(), request.PageNumber, 10);

            NextPage = request.NextPage;
            PreviousPage = request.PreviousPage;
            FirstPage = request.FirstPage;
            LastPage = request.LastPage;
            PageNumber = request.PageNumber;
            PageCount = ClubsPaginacao.PageCount;
        }

        public async Task Refresh()
        {
            ClubsPaginacao.Refresh();

            if (NextPage)
                ClubsPaginacao.ProximaPagina();

            else if (PreviousPage)
                ClubsPaginacao.VoltarPagina();

            else if (FirstPage)
                ClubsPaginacao.PrimeiraPagina();

            else if (LastPage)
                ClubsPaginacao.UltimaPagina();

            
        }

        public List<Club> ObterListaPaginada() => ClubsPaginacao.ObterListaPaginada();

    }

    public class PaginacaoClubViewModelRequest
    {
        public bool NextPage { get; set; }
        public bool PreviousPage { get; set; }
        public bool FirstPage { get; set; }
        public bool LastPage { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public List<Club> ClubsPaginacao { get; set; }
        public List<Club> TodosClubs { get; set; }


    }
}
