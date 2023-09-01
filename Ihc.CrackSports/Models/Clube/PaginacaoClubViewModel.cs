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

        public Paginacao<Club> ClubsPaginacao { get; set; }

        public void Refresh()
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

        public void SetInstancePaginacaoAluno(Paginacao<Club> value)
        {
            ClubsPaginacao = value;
        }
    }
}
