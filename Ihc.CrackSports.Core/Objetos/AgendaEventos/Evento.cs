using Ihc.CrackSports.Core.Extensions;
using Ihc.CrackSports.Core.Objetos.AgendaEventos;
using Ihc.CrackSports.Core.Objetos.AgendaEventos.Dto;
using Ihc.CrackSports.Core.Objetos.Base.Pessoas;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Services;
using Ihc.CrackSports.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Objetos.Competicoes
{
    public class Evento
    {
        public long IdEvento { get; set; }
        public DateTime Data { get; set; }
        public TipoEvento Tipo { get; set; }
        public long IdClub1 { get; set; }
        public long IdClub2 { get; set; }
        public string EnderecoResumido { get; set; }
        public string ImagemBase64 { get; set; }
        public string Observacoes { get; set; }
        public int GolsClub1 { get; set; }
        public int GolsClub2 { get; set; }
        public bool IsEncerrado { get; set; }
        public TimeSpan HoraEvento { get; set; }
        public Club Clube1
        {
            get;
            private set;
        }
        public Club Clube2
        {
            get;
            private set;
        }



        public Evento()
        {
            Data = DateTime.Parse($"01/01/{DateTime.Now.Year}");

        }


        public void InformarClube(Club club)
        {
            Clube1 = club;
        }
        public void InformarClube2(Club club)
        {
            Clube2 = club;
        }


        public bool IsValido() => IdClub1 > 0 && IdClub2 > 0;


        public static Evento ConvertToEvento(EventoDto evento)
        {
            return new Evento()
            {
                IdEvento = evento.id_evento,
                IdClub1 = evento.id_club1,
                IdClub2 = evento.id_club2,
                Data = evento.data_hora,
                EnderecoResumido = evento.endereco_resumido,
                GolsClub1 = evento.gols_club1,
                GolsClub2 = evento.gols_club2,
                ImagemBase64 = evento.imagem_base64,
                IsEncerrado = evento.is_encerrado,
                Observacoes = evento.observacoes,
                Tipo = (TipoEvento)evento.tipo_evento,
                HoraEvento = !string.IsNullOrEmpty(evento.hora_evento) ? TimeSpan.Parse(evento.hora_evento) : new TimeSpan(0)
            };
        }


        public override string ToString()
        {
            StringBuilder str = new StringBuilder();


            str.Append($"{Tipo.GetEnumDescription()} - {Data.ObterDataEscrita()}");


            return str.ToString();
        }
    }
}
