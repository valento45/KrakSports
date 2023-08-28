using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Base.Pessoas
{
    public class PessoaFisica : PessoaBase
    {

        public DateTime DataNascimento { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public bool IsPCD { get; set; }
        public string DescricaoPCD { get; set; }

        public int Idade
        {
            get
            {
                int idade = DateTime.Now.Year - DataNascimento.Year;

                if (DateTime.Now.DayOfYear < DataNascimento.DayOfYear)
                    idade = idade - 1;

                return idade;
            }
        }
    }
}
