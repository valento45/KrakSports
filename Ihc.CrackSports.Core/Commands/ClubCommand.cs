using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using Ihc.CrackSports.Core.Responses.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Commands
{
    public class ClubCommand : IClubCommand
    {
        protected readonly IClubRepository _clubRepository;

        public ClubCommand(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }


        public async Task<CadastroResponse> Salvar(Club club)
        {
            var response = new CadastroResponse();

            if (club.Id > 0)
            {
                if (await _clubRepository.Atualizar(club))
                    response.StatusCode = 200;
                response.IsInsert = false;
            }
            else
            {
                if (await _clubRepository.Incluir(club))
                    response.StatusCode = 200;
				response.IsInsert = true;
			}

            return response;
        }

        public async Task<CadastroResponse> Excluir(long idClub)
        {
            var response = new CadastroResponse();

            if (await _clubRepository.Excluir(idClub))
                response.StatusCode = 200;

            return response;
        }

        public async Task<Club?> ObterById(long idClub)
        {
            return await _clubRepository.ObterById(idClub);
        }

        public async Task<List<Club>?> ObterByNome(string nome, int limite)
        {
            return await _clubRepository.ObterByNome(nome, limite);
        }

        public async Task<Club?> ObterByIdUsuario(long idUsuario)
        {
            var result = await _clubRepository.ObterByIdUsuario(idUsuario);
            if (result != null)
                result.IdUsuario = idUsuario;

            return result;
        }

        public async Task<List<Club>?> ObterTodos(int limite, bool exibeSomenteAtivos = false)
        {
            var result = await _clubRepository.ObterTodos(limite, exibeSomenteAtivos);
            return result.ToList();
        }
    }
}
