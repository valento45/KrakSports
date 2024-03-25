using Ihc.CrackSports.Core.Extensions;
using Ihc.CrackSports.Core.Objetos.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Base.Pessoas
{
    public  class PessoaBase
    {
        public long Id { get; set; }
        /// <summary>
        /// Nome pessoa fisica ou nome fantasia da pessoa juridica
        /// </summary>
        public string Nome { get; set; }
        public bool IsPj { get; set; }
        public long CpfCnpj { get; set; }
        public string CpfCnpjString
        {
            get { return CpfCnpj.ToString(); }
            set
            {

                CpfCnpj = !string.IsNullOrEmpty(value.SomenteNumeros()) ? long.Parse(value.SomenteNumeros()) : 0;

            }
        }
        public Endereco Endereco { get; set; }
        public bool IsVerificado { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

        public string Celular { get; set; }
    }
}
