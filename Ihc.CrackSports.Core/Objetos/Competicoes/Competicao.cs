using Ihc.CrackSports.Core.Objetos.Base.Pessoas;
using Ihc.CrackSports.Core.Objetos.Clube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Competicoes
{
    public class Competicao
    {
        public long IdClubAdversario { get; set; }
        public long IdClubAdversario_2 { get; set; }
        public DateTime DataHoraEvento { get; set; }
        public int Rodada { get; set; }
        public Endereco EnderecoEvento { get; set; }
    }
}
