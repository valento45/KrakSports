using Ihc.CrackSports.Core.Objetos.AgendaEventos;
using Ihc.CrackSports.Core.Objetos.AgendaEventos.Dto;
using Ihc.CrackSports.Core.Objetos.Competicoes;
using Ihc.CrackSports.Core.Repositorys.Base;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Repositorys
{
    public class EventoRepository : RepositoryBase, IEventoRepository
    {
        public EventoRepository(IDbConnection connection) : base(connection)
        {

        }

        public async Task<bool> IncluirEvento(Evento evento)
        {
            string query = "insert into sys.agenda_evento_tb (data_hora, tipo_evento, id_club1, id_club2, endereco_resumido, imagem_base64, observacoes)" +
                " values(@data_hora, @tipo_evento, @id_club1, @id_club2, @endereco_resumido, @imagem_base64, @observacoes) returning id_evento";

            NpgsqlCommand cmd = new NpgsqlCommand(query);
            cmd.Parameters.AddWithValue(@"data_hora", evento.DataHora);
            cmd.Parameters.AddWithValue(@"tipo_evento", (int)evento.Tipo);
            cmd.Parameters.AddWithValue(@"id_club1", evento.IdClub1);
            cmd.Parameters.AddWithValue(@"id_club2", evento.IdClub2);
            cmd.Parameters.AddWithValue(@"endereco_resumido", evento.EnderecoResumido);
            cmd.Parameters.AddWithValue(@"imagem_base64", evento?.ImagemBase64 ?? "");
            cmd.Parameters.AddWithValue(@"observacoes", evento?.Observacoes ?? "");

            var result = await base.ExecuteScalarAsync(cmd);

            long idEventReturned;
            if(long.TryParse(result?.ToString(), out idEventReturned))
            {
                evento.IdEvento = idEventReturned;
                return true;
            }

            return false;
        }

        public async Task<bool> AtualizarEvento(Evento evento)
        {
            string query = "update sys.agenda_evento_tb set data_hora = @data_hora, tipo_evento = @tipo_evento, id_club1 = @id_club1, id_club2 = @id_club2, endereco_resumido = @endereco_resumido," +
                " imagem_base64 = @imagem_base64, observacoes = @observacoes where id_evento = @id_evento";

            return await base.ExecuteAsync(query,
                new
                {
                    id_evento = evento.IdEvento,
                    data_hora = evento.DataHora,
                    tipo_evento = (int)evento.Tipo,
                    id_club1 = evento.IdClub1,
                    id_club2 = evento.IdClub2,
                    endereco_resumido = evento.EnderecoResumido,
                    imagem_base64 = evento.ImagemBase64,
                    observacoes = evento.Observacoes
                });
        }

        public async Task<bool> ExcluirEvento(long IdEvento)
        {
            string query = "delete from sys.agenda_evento_tb where id_evento = " + IdEvento;
            return await base.ExecuteAsync(query);
        }

        public async Task<IEnumerable<Evento>> GetEventos(DateTime dataInicio, DateTime dataFim)
        {
            string query = $"select * from sys.agenda_evento_tb where data_hora" +
                $" between timestamp '{dataInicio.ToString("yyyy-MM-dd HH:mm:ss")}' and timestamp '{dataFim.ToString("yyyy-MM-dd HH:mm:ss")}' ";

            var result = await base.QueryAsync<EventoDto>(query);

            return result?.Select(x => x.ToEvento())?.OrderBy(x => x.DataHora)?.AsEnumerable() ?? new List<Evento>();
        }


        public async Task<IEnumerable<Evento>> GetEventosByIdClube(long IdClube)
        {
            string query = $"select * from sys.agenda_evento_tb where id_club1 = {IdClube} OR id_club2 = {IdClube}";

            var result = await base.QueryAsync<EventoDto>(query);

            return result?.Select(x => x.ToEvento())?.OrderBy(x => x.DataHora)?.AsEnumerable() ?? new List<Evento>();
        }

        public async Task<Evento> GetEventoById(long IdEvento)
        {
            string query = $"select * from sys.agenda_evento_tb where id_evento = {IdEvento} LIMIT 1";

            var result = await base.QueryAsync<EventoDto>(query);

            return result?.Select(x => x.ToEvento())?.FirstOrDefault() ?? null;
        }

        public Task<bool> LancarPlacarEvento(List<GolsEventoAtleta> golsMarcados, bool isEncerrado = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EncerrarEvento(long idEvento)
        {
            throw new NotImplementedException();
        }
    }
}
