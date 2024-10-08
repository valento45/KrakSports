﻿using Ihc.CrackSports.Core.Objetos.AgendaEventos;
using Ihc.CrackSports.Core.Objetos.Competicoes;
using Ihc.CrackSports.Core.Responses.AgendaEventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Services.Interfaces
{
    public interface IEventoService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="evento"></param>
        /// <returns></returns>
        Task<bool> Salvar(Evento evento);
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEvento"></param>
        /// <returns></returns>
        Task<bool> ExcluirEvento(long IdEvento);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEvento"></param>
        /// <returns></returns>
        Task<Evento> GetEventoById(long IdEvento);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="idEvento"></param>
        /// <returns></returns>
        Task<bool> EncerrarEvento(long idEvento);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataInicio"></param>
        /// <param name="dataFim"></param>
        /// <returns></returns>
        Task<EventosResponse> GetEventos(DateTime dataInicio, DateTime dataFim);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdClube"></param>
        /// <returns></returns>
        Task<EventosResponse> GetEventosByIdClube(long IdClube);

        

        Task<bool> ExcluirLancamentoPlacar(long idLancamento);
    }
}
