using Ihc.CrackSports.Core.Objetos.Colaborador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Repositorys.Interfaces
{
    public interface IColaboradorRepository
    {
        Task<IEnumerable<Patrocinador>> GetAll();
        Task<IEnumerable<Patrocinador>> GetAllAtivos();
        Task<bool> NovoPatrocinador(Patrocinador patrocinador);
        Task<bool> AtualizarPatrocinador(Patrocinador patrocinador);
        Task<bool> ExcluirPatrocinador(long idPatrocinador);
        Task<IEnumerable<Patrocinador>> GetAllPendentes();

    }
}
