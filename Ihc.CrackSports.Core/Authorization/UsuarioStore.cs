using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using Ihc.CrackSports.Core.Objetos.Enums;
using System.Reflection.Metadata.Ecma335;
using Ihc.CrackSports.Core.Services.Interfaces;

namespace Ihc.CrackSports.Core.Authorization
{
    public class UsuarioStore : IUserStore<Usuario>, IUserPasswordStore<Usuario>
    {
        protected readonly IDbConnection _connection;
        protected readonly IUsuarioService _usuarioService;

        public UsuarioStore(IDbConnection connection, IUsuarioService usuarioService)
        {
            _connection = connection;
            _usuarioService = usuarioService;
        }


        public async Task<IdentityResult> CreateAsync(Usuario user, CancellationToken cancellationToken)
        {

            var inserted = await _connection.ExecuteAsync("insert into sys.usuario_tb (login, normalizedLogin, senha,  email) values (@login, @normalizedLogin, @senha,  @email)",
                new
                {
                    login = user.UserName,
                    normalizedLogin = user.NormalizedUserName,
                    senha = user.PasswordHash,
                    email = user.Email
                });

            if (inserted > 0)
                return IdentityResult.Success;
            else
                return IdentityResult.Failed(new IdentityError[] { new IdentityError() { Description = "Erro ao inserir o usuário." } });

        }
        public async Task<IdentityResult> UpdateAsync(Usuario user, CancellationToken cancellationToken)
        {

            var inserted = await _connection.ExecuteAsync("update sys.usuario_tb set login = @login, senha = @senha, tipo = @tipo, email = @email " +
                "where id_usuario = @id_usuario",
                new
                {
                    id_usuario = user.Id,
                    login = user.UserName,
                    normalizedLogin = user.NormalizedUserName,
                    senha = user.PasswordHash,
                    email = user.Email
                });

            if (inserted > 0)
                return IdentityResult.Success;
            else
                return IdentityResult.Failed(new IdentityError[] { new IdentityError() { Description = "Erro ao inserir o usuário." } });

        }

        public async Task<IdentityResult> DeleteAsync(Usuario user, CancellationToken cancellationToken)
        {

            var deleted = await _connection.ExecuteAsync("delete from sys.usuario_tb where id_usuario = @id_usuario",
                new
                {
                    id_usuario = user.Id
                });

            if (deleted > 0)
                return IdentityResult.Success;
            else
                return IdentityResult.Failed(new IdentityError[] { new IdentityError() { Description = "Erro ao excluir o usuário." } });

        }

        public void Dispose()
        {

        }

        public Task<Usuario> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {

            return await _usuarioService.ObterPorUserName(normalizedUserName);
        }

        public Task<string> GetNormalizedUserNameAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(Usuario user, CancellationToken cancellationToken)
        {


            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetUserIdAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(
                !string.IsNullOrEmpty(user.PasswordHash)
                );
        }

        public Task SetNormalizedUserNameAsync(Usuario user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(Usuario user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(Usuario user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }


    }
}
