﻿using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Objetos.Colaborador;
using Ihc.CrackSports.Core.Utils.Paginacoes;


namespace Ihc.CrackSports.Core.ViewModel.Colaborador
{
    public class PaginacaoPatrocinadorViewModel
    {
        public bool NextPage { get; set; }
        public bool PreviousPage { get; set; }
        public bool FirstPage { get; set; }
        public bool LastPage { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }

        public Paginacao<Patrocinador> PatrocinadorPaginacao { get; set; }
        public List<Patrocinador> TodosPatrocinadores
        {
            get
            {
                return PatrocinadorPaginacao.ObterListaCompleta().ToList();
            }
        }


        public PaginacaoPatrocinadorViewModel()
        {
            
        }


        public PaginacaoPatrocinadorViewModel(PaginacaoPatrocinadorViewModelRequest request)
        {
            PatrocinadorPaginacao = new Paginacao<Patrocinador>(request.TodosPatrocinadores.AsQueryable(), request.PageNumber > 0 ? request.PageNumber : 1, 6);

            NextPage = request.NextPage;
            PreviousPage = request.PreviousPage;
            FirstPage = request.FirstPage;
            LastPage = request.LastPage;
            PageNumber = request.PageNumber;
            PageCount = PatrocinadorPaginacao.PageCount;
        }

        public PaginacaoPatrocinadorViewModel(Paginacao<Patrocinador> paginacao)
        {
            PatrocinadorPaginacao = paginacao;
            PageNumber = paginacao.PageNumber;
            PageCount = paginacao.PageCount;
        }

        public PaginacaoPatrocinadorViewModel(PaginacaoPatrocinadorViewModel request)
        {
            
            PatrocinadorPaginacao = request.PatrocinadorPaginacao;        
            NextPage = request.NextPage;
            PreviousPage = request.PreviousPage;
            FirstPage = request.FirstPage;
            LastPage = request.LastPage;
            PageNumber = request.PageNumber;
            PageCount = request.PageCount;
        }


        public async Task Refresh()
        {
            PatrocinadorPaginacao.Clear();

            if (NextPage)
            {
                PatrocinadorPaginacao.ProximaPagina();
                NextPage = false;
            }
            else if (PreviousPage)
            {
                PatrocinadorPaginacao.VoltarPagina();
                PreviousPage = false;
            }
            else if (FirstPage)
            {
                PatrocinadorPaginacao.PrimeiraPagina();
                FirstPage = false;
            }
            else if (LastPage)
            {
                PatrocinadorPaginacao.UltimaPagina();
                LastPage = false;
            }
            else            
                PatrocinadorPaginacao.PrimeiraPagina();
            
        }

        public List<Patrocinador> ObterListaPaginada() => PatrocinadorPaginacao.ObterListaPaginada();
    }

    public class PaginacaoPatrocinadorViewModelRequest
    {
        public bool NextPage { get; set; }
        public bool PreviousPage { get; set; }
        public bool FirstPage { get; set; }
        public bool LastPage { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public List<Patrocinador> PatrocinadorPaginacao { get; set; }
        public List<Patrocinador> TodosPatrocinadores { get; set; }
    }
}
