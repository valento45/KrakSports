using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Responses.Usuarios;
using Ihc.CrackSports.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Services
{
    public class ClubService : IClubService
    {
        private readonly IClubCommand _clubCommand;

        public ClubService(IClubCommand clubCommand)
        {
            _clubCommand = clubCommand;
        }

        public async Task<CadastroResponse> Salvar(Club club)
        {
            return await _clubCommand.Salvar(club);
        }
        public async Task<CadastroResponse> Excluir(long idClub)
        {
           return await _clubCommand.Excluir(idClub);
        }

        public async Task<Club?> ObterById(long idClub)
        {
            return await _clubCommand.ObterById(idClub);
        }

        public async Task<List<Club>?> ObterByNome(string nome, int limite)
        {
            return await _clubCommand.ObterByNome(nome, limite);
        }

		public Task<Club?> ObterByIdUsuario(long idUser)
		{
			throw new NotImplementedException();
		}
	}
}
