using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Responses.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Commands.Interfaces
{
    public interface IClubCommand
    {


        Task<CadastroResponse> Salvar(Club club);     
        Task<CadastroResponse> Excluir(long idClub);

        Task<Club?> ObterById(long idClub);
        Task<Club?> ObterByIdUsuario(long idUsuario);
        Task<List<Club>?> ObterByNome(string nome, int limite);
        Task<List<Club>?> ObterTodos( int limite);
    }
}
