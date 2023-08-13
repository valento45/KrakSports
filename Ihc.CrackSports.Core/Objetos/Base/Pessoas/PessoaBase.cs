using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Base.Pessoas
{
    public abstract class PessoaBase
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public bool IsPj { get; set; }
        public long CpfCnpj { get; set; }
        public string Endereco { get; set; }
        public int Numero { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
    }
}
