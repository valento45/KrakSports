using Ihc.CrackSports.Core.Objetos.Base.Pessoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Alunos
{
    public class Responsavel : PessoaFisica
    {
        public long IdAluno { get; set; }
        public string GrauParentesco { get; set; }

        public Responsavel()
        {
            
        }

        public Responsavel(string nome, long cpfCnpj, string documento, string grauParentesco, string telefone, string celular)
        {
            Nome = nome;
            CpfCnpj = cpfCnpj;
            Documento = documento;
            GrauParentesco = grauParentesco;
            Telefone = telefone;
            Celular = celular;

        }
    }
}
