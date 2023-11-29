using Ihc.CrackSports.Core.Objetos.Colaborador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Commands.Interfaces
{
    public interface IColaboradorCommand
    {
        Task<IEnumerable<Patrocinador>> GetAll();
        Task<IEnumerable<Patrocinador>> GetAllAtivos();
        Task<bool> NovoPatrocinador(Patrocinador patrocinador);
        Task<bool> AtualizarPatrocinador(Patrocinador patrocinador);
        Task<bool> ExcluirPatrocinador(long idPatrocinador);
        Task<IEnumerable<Patrocinador>> GetAllPendentes();
        Task<IEnumerable<Patrocinador>> GetAllInativos();
    }
}
