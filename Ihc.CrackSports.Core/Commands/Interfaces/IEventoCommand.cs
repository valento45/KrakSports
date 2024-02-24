using Ihc.CrackSports.Core.Objetos.AgendaEventos;
using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Competicoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Commands.Interfaces
{
    public interface IEventoCommand
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="evento"></param>
        /// <returns></returns>
        Task<bool> IncluirEvento(Evento evento);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="evento"></param>
        /// <returns></returns>
        Task<bool> AtualizarEvento(Evento evento);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEvento"></param>
        /// <returns></returns>
        Task<bool> ExcluirEvento(long IdEvento);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataInicio"></param>
        /// <param name="dataFim"></param>
        /// <returns></returns>
        Task<IEnumerable<Evento>> GetEventos(DateTime dataInicio, DateTime dataFim);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEvento"></param>
        /// <returns></returns>
        Task<Evento> GetEventoById(long IdEvento);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdClube"></param>
        /// <returns></returns>
        Task<IEnumerable<Evento>> GetEventosByIdClube(long IdClube);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="golsMarcados"></param>
        /// <param name="isEncerrado"></param>
        /// <returns></returns>
        Task<bool> LancarPlacarEvento(AtletaEvento atletaEvento, bool isEncerrado = false);

        Task<IEnumerable<AtletaEvento>> ObterPlacar(long idEvento);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idEvento"></param>
        /// <returns></returns>
        Task<bool> EncerrarEvento(long idEvento);

        Task<bool> EscalarTime(List<Aluno> time, long idEvento, long idClub);

        Task<bool> LimparEscalacaoTime(long idEvento, long idClub);
    }
}
