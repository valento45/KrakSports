using Ihc.CrackSports.Core.Objetos.Alunos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.AgendaEventos
{
    public class AtletaEvento
    {
        private Aluno _dadosAtleta;

        public long IdEvento { get; set; }
        public long IdAluno { get; set; }
        public long IdClube { get; set; }

        public Aluno DadosAtleta
        {
            get
            {
                if ((_dadosAtleta == null || _dadosAtleta.Id <= 0) && IdAluno > 0)
                {
                    _dadosAtleta = Aluno.GetById(IdAluno); 
                }


                return _dadosAtleta;
            }
            set
            {
                _dadosAtleta = value;
            }
        }

        public AtletaEvento()
        {
            DadosAtleta = new Aluno();
        }
    }
}
