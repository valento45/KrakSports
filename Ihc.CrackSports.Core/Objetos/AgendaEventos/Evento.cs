using Ihc.CrackSports.Core.Extensions;
using Ihc.CrackSports.Core.Objetos.AgendaEventos;
using Ihc.CrackSports.Core.Objetos.AgendaEventos.Dto;
using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Base.Pessoas;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Objetos.Notifications.Base;
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
        private Club club1;
        private Club club2;

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
            get
            {
                if ((club1 == null || club1.Id <= 0) && IdClub1 > 0)
                    club1 = Club.ObterClube(IdClub1);

                return club1;
            }
            set
            {
                club1 = value;
            }
        }
        public Club Clube2
        {
            get
            {
                if ((club2 == null || club2.Id <= 0) && IdClub2 > 0)
                    club2 = Club.ObterClube(IdClub2);

                return club2;
            }
            set
            {
                club2 = value;
            }
        }

        public Evento()
        {
            Data = DateTime.Parse($"01/01/{DateTime.Now.Year}");
            Clube1 = new Club();
            Clube2 = new Club();
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

        public NotificationBase ObterNotificacao(Club club)
        {
            if (club != null)
            {

                NotificationBase notification = new NotificationBase();
                notification.Notificacao = "Seu clube foi classificado para um evento.";
                notification.Tipo = Notifications.TipoNotificacao.Evento;
                notification.IdClube = club.Id;
                notification.LinkRedirect = $"../Evento/VerEvento?idEvento={IdEvento}";
                notification.TipoUsuario = Enums.TipoUsuario.Club;

                return notification;
            }
            return null;
        }
    }
}
