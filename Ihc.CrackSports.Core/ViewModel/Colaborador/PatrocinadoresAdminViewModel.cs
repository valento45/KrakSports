using Ihc.CrackSports.Core.Objetos.Colaborador;
using Ihc.CrackSports.Core.Utils.Paginacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.ViewModel.Colaborador
{
    public class PatrocinadoresAdminViewModel
    {
       public PaginacaoPatrocinadorViewModel Ativos { get; private set; }
        public PaginacaoPatrocinadorViewModel Inativos { get; private set; }
        public PaginacaoPatrocinadorViewModel Solicitacoes { get; private set; }


        public void InformarAtivos(PaginacaoPatrocinadorViewModel ativos)
        {
            Ativos = ativos;
        }

        public void InformarInativos(PaginacaoPatrocinadorViewModel inativos)
        {
            Inativos = inativos;
        }

        public void InformarSolicitacoes(PaginacaoPatrocinadorViewModel solicitacoes)
        {
            Solicitacoes = solicitacoes;    
        }

        public async  Task<PaginacaoPatrocinadorViewModel> InicializarPaginacao(IEnumerable<Patrocinador> superser)
        {
            Paginacao<Patrocinador> paginacao = new Paginacao<Patrocinador>(superser.AsQueryable(), 1, 10);
            PaginacaoPatrocinadorViewModel res = new PaginacaoPatrocinadorViewModel(paginacao);

            await res.Refresh();

            return res;
        }
    }
}
