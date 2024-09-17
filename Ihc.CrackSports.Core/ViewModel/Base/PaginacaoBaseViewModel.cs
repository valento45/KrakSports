using Ihc.CrackSports.Core.Objetos.Colaborador;
using Ihc.CrackSports.Core.Utils.Paginacoes;
using Ihc.CrackSports.Core.ViewModel.Colaborador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.ViewModel.Base
{
    public class PaginacaoBaseViewModel<T>
    {
        public bool NextPage { get; set; }
        public bool PreviousPage { get; set; }
        public bool FirstPage { get; set; }
        public bool LastPage { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }

        public Paginacao<T> ObjetoPaginacao { get; set; }
        public List<T> TodosObjetos
        {
            get
            {
                return ObjetoPaginacao.ObterListaCompleta().ToList();
            }
        }


        public PaginacaoBaseViewModel()
        {

        }


        public PaginacaoBaseViewModel(PaginacaoBaseViewModelRequest<T> request)
        {

            int pageSize = request.PageSize > 0 ? request.PageSize : 6;
            int pageNumber = request.PageNumber > 0 ? request.PageNumber : 1;

            ObjetoPaginacao = new Paginacao<T>(request.TodosObjetos.AsQueryable(),
                pageNumber, pageSize);

            NextPage = request.NextPage;
            PreviousPage = request.PreviousPage;
            FirstPage = request.FirstPage;
            LastPage = request.LastPage;
            PageNumber = request.PageNumber;
            PageSize = pageSize;

            PageCount = ObjetoPaginacao.PageCount;
        }

        public PaginacaoBaseViewModel(Paginacao<T> paginacao)
        {
            ObjetoPaginacao = paginacao;
            PageNumber = paginacao.PageNumber;
            PageCount = paginacao.PageCount;
        }

        public PaginacaoBaseViewModel(PaginacaoBaseViewModel<T> request)
        {

            ObjetoPaginacao = request.ObjetoPaginacao;
            NextPage = request.NextPage;
            PreviousPage = request.PreviousPage;
            FirstPage = request.FirstPage;
            LastPage = request.LastPage;
            PageNumber = request.PageNumber;
            PageCount = request.PageCount;
        }


        public async Task Refresh()
        {
            ObjetoPaginacao.Clear();

            if (NextPage)
            {
                ObjetoPaginacao.ProximaPagina();
                NextPage = false;
            }
            else if (PreviousPage)
            {
                ObjetoPaginacao.VoltarPagina();
                PreviousPage = false;
            }
            else if (FirstPage)
            {
                ObjetoPaginacao.PrimeiraPagina();
                FirstPage = false;
            }
            else if (LastPage)
            {
                ObjetoPaginacao.UltimaPagina();
                LastPage = false;
            }
            else
                ObjetoPaginacao.PrimeiraPagina();

            this.PageNumber = ObjetoPaginacao.PageNumber;

        }

        public List<T> ObterListaPaginada() => ObjetoPaginacao.ObterListaPaginada();
    }

    public class PaginacaoBaseViewModelRequest<T>
    {
        public bool NextPage { get; set; }
        public bool PreviousPage { get; set; }
        public bool FirstPage { get; set; }
        public bool LastPage { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public List<T> ObjetoPaginacao { get; set; }
        public List<T> TodosObjetos { get; set; }
    }
}
