using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Base.Pessoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Clube
{
    public class Club : PessoaJuridica
    {      
        public string ImagemBase64 { get; set; }
        public long IdUsuario { get; set; }
        public DateTime DataFundacao { get; set; }
        public string NomePresidente { get; set; }

        public Club()
        {
            
        }

        public bool HasImagem()
            => !string.IsNullOrEmpty(ImagemBase64);  

    }
}
