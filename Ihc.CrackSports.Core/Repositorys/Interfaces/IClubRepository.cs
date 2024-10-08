﻿using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Objetos.Clube;
using Ihc.CrackSports.Core.Responses.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Repositorys.Interfaces
{
    public interface IClubRepository
    {
        Task<bool> Incluir(Club club);
        Task<bool> Atualizar(Club club);
        Task<bool> Excluir(long idClub);
        Task<Club?> ObterById(long idClub);
        Task<Club?> ObterByIdUsuario(long idUsuario);
        Task<List<Club>?> ObterByNome(string nome, int limite = 0);
        Task<bool> AceitarAlunoClub(SolicitacaoAlunoClub solicitacao);
        Task<IEnumerable<Club>> ObterTodos(int limite, bool exibeSomenteAtivos = false);
    }
}
