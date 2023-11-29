using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Colaborador;
using Ihc.CrackSports.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Services
{
    public class ColaboradorService : IColaboradorService
    {
        private readonly IColaboradorCommand _colaboradorCommand;

        public ColaboradorService(IColaboradorCommand colaboradorCommand)
        {
            _colaboradorCommand = colaboradorCommand;
        }
        public async Task<bool> AtualizarPatrocinador(Patrocinador patrocinador)
        {
            return await _colaboradorCommand.AtualizarPatrocinador(patrocinador);
        }

        public async Task<bool> ExcluirPatrocinador(long idPatrocinador)
        {
            return await _colaboradorCommand.ExcluirPatrocinador(idPatrocinador);
        }

        public async Task<IEnumerable<Patrocinador>> GetAll()
        {
            return await _colaboradorCommand
                .GetAll();
        }

        public async Task<IEnumerable<Patrocinador>> GetAllAtivos()
        {
            return await _colaboradorCommand .GetAllAtivos();
        }

        public async Task<IEnumerable<Patrocinador>> GetAllInativos()
        {
            return await _colaboradorCommand.GetAllInativos();
        }

        public async Task<IEnumerable<Patrocinador>> GetAllPendentes()
        {
            return await _colaboradorCommand.GetAllPendentes();
        }

        public async Task<bool> NovoPatrocinador(Patrocinador patrocinador)
        {
            patrocinador.LogoTipoBase64 = patrocinador.LogoTipoBase64;


            return await _colaboradorCommand.NovoPatrocinador(patrocinador);
        }
    }
}
