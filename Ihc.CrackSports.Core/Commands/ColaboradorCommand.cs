using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Colaborador;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Commands
{
    public class ColaboradorCommand : IColaboradorCommand
    {
        private readonly IColaboradorRepository _colaboradorRepository;

        public ColaboradorCommand(IColaboradorRepository colaboradorRepository)
        {
            _colaboradorRepository = colaboradorRepository;
        }

        public async Task<bool> AtualizarPatrocinador(Patrocinador patrocinador)
        {
            return await _colaboradorRepository.AtualizarPatrocinador(patrocinador);
        }

        public async Task<bool> ExcluirPatrocinador(long idPatrocinador)
        {
            return await _colaboradorRepository.ExcluirPatrocinador(idPatrocinador);
        }

        public async Task<IEnumerable<Patrocinador>> GetAll()
        {
            return await _colaboradorRepository.GetAll();
        }

        public async Task<IEnumerable<Patrocinador>> GetAllAtivos()
        {
            return await _colaboradorRepository.GetAllAtivos();
        }

        public async Task<IEnumerable<Patrocinador>> GetAllInativos()
        {
            return await _colaboradorRepository.GetAllInativos();
        }

        public async Task<IEnumerable<Patrocinador>> GetAllPendentes()
        {
            return await _colaboradorRepository.GetAllPendentes();
        }

        public async Task<bool> NovoPatrocinador(Patrocinador patrocinador)
        {
            return await _colaboradorRepository.NovoPatrocinador(patrocinador);
        }
    }
}
