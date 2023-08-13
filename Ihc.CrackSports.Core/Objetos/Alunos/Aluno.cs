using Ihc.CrackSports.Core.Objetos.Base.Pessoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Alunos
{
    public class Aluno : PessoaFisica
    {
        public long IdClub { get; set; }
        public long IdUsuario { get; set; }
        public string PosicaoJogador { get; set; }
        public int CamisetaNumero { get; set; }
        public string NomeResponsavel { get; set; }
        public string DocumentoResponsavel { get; set; }
        public long CpfResponsavel { get; set; }
        public string GrauParentesco { get; set; }

    }
}
