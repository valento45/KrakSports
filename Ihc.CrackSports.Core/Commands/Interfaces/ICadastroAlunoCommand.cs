using Ihc.CrackSports.Core.Requests.Aluno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Commands.Interfaces
{
    public interface ICadastroAlunoCommand
    {
        Task<bool> IncluirCadastroAluno(CadastroRequest cadastroRequest);
        Task<bool> AtualizarCadastroAluno(CadastroRequest cadastroRequest);
    }
}
