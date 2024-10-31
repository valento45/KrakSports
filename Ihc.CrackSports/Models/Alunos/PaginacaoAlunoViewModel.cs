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
        public bool FirstPage { get; set; }
        public bool LastPage { get; set; }
        public bool CanUpdate { get; set; } = true;
        public Paginacao<Aluno> AlunosPaginado { get; private set; }

        public PaginacaoAlunoViewModel()
        {

        }
        public PaginacaoAlunoViewModel(IEnumerable<Aluno> alunos)
        {
            AlunosPaginado = new Paginacao<Aluno>(alunos.AsQueryable(), 1, 10);

            Refresh();
        }

        public void Refresh()
        {
            AlunosPaginado.Clear();

            if (NextPage)
                AlunosPaginado.ProximaPagina();

            else if (PreviousPage)
                AlunosPaginado.VoltarPagina();

            else if (FirstPage)
                AlunosPaginado.PrimeiraPagina();

            else if (LastPage)
                AlunosPaginado.UltimaPagina();
            else
                AlunosPaginado.PrimeiraPagina();
        }

        public List<Aluno> ObterListaPaginada() => AlunosPaginado.ObterListaPaginada();

        public void SetInstancePaginacaoAluno(Paginacao<Aluno> value)
        {
            AlunosPaginado = value;
        }
    }
}
