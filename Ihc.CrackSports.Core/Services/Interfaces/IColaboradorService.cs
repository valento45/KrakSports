using Ihc.CrackSports.Core.Objetos.Colaborador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Services.Interfaces
{
    public interface IColaboradorService
    {
        Task<IEnumerable<Patrocinador>> GetAll();
        Task<IEnumerable<Patrocinador>> GetAllAtivos();
        Task<bool> NovoPatrocinador(Patrocinador patrocinador);
        Task<bool> AtualizarPatrocinador(Patrocinador patrocinador);
        Task<bool> ExcluirPatrocinador(long idPatrocinador);
        Task<IEnumerable<Patrocinador>> GetAllPendentes();
    }
}
