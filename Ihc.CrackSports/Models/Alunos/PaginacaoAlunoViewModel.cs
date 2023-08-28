using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Utils.Paginacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.WebApp.Models.Alunos
{
    public class PaginacaoAlunoViewModel
    {
        public bool NextPage { get; set; }
        public bool PreviousPage { get; set; }  
        public bool FirstPage { get;set; }
        public bool LastPage { get; set; }

        public Paginacao<Aluno> AlunosPaginado { get; private set; }

        public void Refresh()
        {
            AlunosPaginado.Refresh();

            if (NextPage)
                AlunosPaginado.ProximaPagina();

            else if (PreviousPage)
                AlunosPaginado.VoltarPagina();

            else if (FirstPage)
                AlunosPaginado.PrimeiraPagina();

            else if (LastPage)
                AlunosPaginado.UltimaPagina();            
        }

        public List<Aluno> ObterListaPaginada() => AlunosPaginado.ObterListaPaginada();

        public void SetInstancePaginacaoAluno(Paginacao<Aluno> value)
        {
            AlunosPaginado = value;
        }
    }
}
