using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Repositorys.Base;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Repositorys
{
    public class ResponsavelRepository : RepositoryBase, IResponsavelRepository
    {
        public ResponsavelRepository(IDbConnection connection) : base(connection)
        {
            
        }

        public Task<bool> Inserir(Responsavel responsavel)
        {
            throw new NotImplementedException();
        }


        public Task<bool> Atualizar(Responsavel responsavel)
        {
            throw new NotImplementedException();
        }
      

        public Task<List<Responsavel>> ObterByDocumento(string rg)
        {
            throw new NotImplementedException();
        }

        public Task<List<Responsavel>> ObterByIdAluno(long idAluno)
        {
            throw new NotImplementedException();
        }

        public Task<List<Responsavel>> ObterByCpf(long cpf)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Excluir(long idResponsavel)
        {
            throw new NotImplementedException();
        }
        public Task<bool> ExcluirByIdAluno(long idAluno)
        {
            throw new NotImplementedException();
        }
    }
}
