using Ihc.CrackSports.Core.Objetos.Alunos;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Utils.Paginacoes
{
    public class Paginacao<T> : PagedList<T>
    {

        public IQueryable<T> Superset { get;  set; }
        public Paginacao(IQueryable<T> superset, int pageNumber, int pageSize) : base(superset, pageNumber, pageSize)
        {
            Superset = superset;
        }


        public void ProximaPagina()
        {          
            if (this.Superset != null && base.TotalItemCount > 0)
            {
                if (PageNumber < base.PageCount)
                    PageNumber += 1;
                else
                    PageNumber = base.PageCount;


                Subset.AddRange(Superset.Skip(PageNumber * PageSize).Take(PageSize).ToList());
            }
        }
        public void VoltarPagina()
        {
           
            if (this.Superset != null && base.TotalItemCount > 0)
            {
                if (PageNumber <= 1)
                    Subset.AddRange(Superset.Skip(0).Take(PageSize).ToList());
                else
                    Subset.AddRange(Superset.Skip(PageNumber * PageSize).Take(PageSize).ToList());
            }
        }

        public void PrimeiraPagina()
        {
            if (this.Superset != null && base.TotalItemCount > 0)
                Subset.AddRange(Superset.Skip(0).Take(PageSize).ToList());
        }  


        public void UltimaPagina()
        {
            if (this.Superset != null && base.TotalItemCount > 0)
                Subset.AddRange(Superset.Skip(PageCount * PageSize).Take(PageSize).ToList());
        }


        public List<T> ObterListaPaginada()
        {
            return this.Subset;
        }


        public void Refresh()
        {
            this.Subset.RemoveAll(x => x != null);           
        }
    }
}
