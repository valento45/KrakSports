﻿using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Requests.Aluno;
using Ihc.CrackSports.Core.Responses.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Services.Interfaces
{
    public interface IUsuarioService
    {

        Task<CadastroResponse> InsertOrUpdate(CadastroRequest request, long idUsuario);      
        Task<CadastroResponse> Excluir(long idAluno);
        Task<Usuario> ObterPorUserName(string userName);
        Task<Usuario> GetById(long id);
        Task<IEnumerable<Usuario>> GetAllAdministradores();
        Task<IEnumerable<Usuario>> GetAll();
        Task<bool> AtualizarUsuario(Usuario usuario);
    }
}
