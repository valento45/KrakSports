using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Requests.Aluno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Commands
{
    public class CadastroAlunoCommand : ICadastroAlunoCommand
    {
        public CadastroAlunoCommand()
        {
            
        }


        public Task<bool> AtualizarCadastroAluno(CadastroRequest cadastroRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IncluirCadastroAluno(CadastroRequest cadastroRequest)
        {
            throw new NotImplementedException();
        }
    }
}
