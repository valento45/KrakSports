using Ihc.CrackSports.Core.Objetos.Alunos;
using System;
using System.Collections.Generic;
using System.Data;
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
        public int GolsMarcados { get; set; }


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



        public AtletaEvento(DataRow dr)
        {
            IdEvento = long.Parse(dr["id_evento"].ToString());
            IdAluno = long.Parse(dr["id_aluno"].ToString());
            IdClube = long.Parse(dr["id_clube"].ToString());
            GolsMarcados = int.Parse(dr["gols_marcados"].ToString());
        }


        public AtletaEvento(Aluno aluno, long idEvento, int golsMarcados)
        {
            _dadosAtleta = aluno;
            IdAluno = aluno.Id;
            IdClube = aluno.IdClub;
            IdEvento = idEvento;
            GolsMarcados = golsMarcados;
        }
    }
}
