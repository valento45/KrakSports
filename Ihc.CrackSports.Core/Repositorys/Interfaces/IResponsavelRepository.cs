using Ihc.CrackSports.Core.Objetos.Alunos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Repositorys.Interfaces
{
    public interface IResponsavelRepository
    {
        Task<bool> Inserir(Responsavel responsavel);
        Task<bool> Atualizar(Responsavel responsavel, long idAluno);
        Task<bool> Excluir(long idResponsavel);
        Task<bool> ExcluirByIdAluno(long idAluno);



        Task<List<Responsavel>> ObterByIdAluno(long idAluno);
        Task<List<Responsavel>> ObterByCpf(long cpf);
        Task<List<Responsavel>> ObterByDocumento(string rg);

    }
}
