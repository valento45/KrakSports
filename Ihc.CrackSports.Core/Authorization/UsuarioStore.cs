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
using System.Security.Claims;
using Ihc.CrackSports.Core.Authorization.Claims;

namespace Ihc.CrackSports.Core.Authorization
{
    public class UsuarioStore : IUserStore<Usuario>, IUserPasswordStore<Usuario>, IUserClaimStore<Usuario>
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

            var inserted = await _connection.ExecuteAsync("insert into sys.usuario_tb (login, normalizedLogin, senha,  email, tipo_usuario)" +
                " values (@login, @normalizedLogin, @senha,  @email, @tipo_usuario)",
                new
                {
                    login = user.UserName,
                    normalizedLogin = user.NormalizedUserName,
                    senha = user.PasswordHash,
                    email = user.Email,
                    tipo_usuario = (int)user.TipoUsuario
                });

            if (inserted > 0)
                return IdentityResult.Success;
            else
                return IdentityResult.Failed(new IdentityError[] { new IdentityError() { Description = "Erro ao inserir o usuário." } });

        }
        public async Task<IdentityResult> UpdateAsync(Usuario user, CancellationToken cancellationToken)
        {

            var inserted = await _connection.ExecuteAsync("update sys.usuario_tb set login = @login, normalizedLogin = @normalizedLogin, senha = @senha, email = @email, " +
                "tipo_usuario = @tipo_usuario  where id_usuario = @id_usuario ",
                new
                {
                    id_usuario = user.Id,
                    login = user.UserName,
                    normalizedLogin = user.NormalizedUserName,
                    senha = user.PasswordHash,
                    email = user.Email,
                    tipo_usuario = (int)user.TipoUsuario
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

        public async Task<Usuario> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var user = await _usuarioService.GetById(long.Parse(userId));
                user.Claims = await this.GetClaimsAsync(user, cancellationToken);
                return user;
            }
            return null;

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
            return Task.FromResult(user.Id.ToString());
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

        public async Task<IList<Claim>> GetClaimsAsync(Usuario user, CancellationToken cancellationToken)
        {
            var result = await _connection.QueryAsync<ClaimDto>("select * from sys.usuario_claim_tb where id_usuario = @id_usuario",
                new
                {
                    id_usuario = user.Id
                });

            return result.Select(x => x.ToClaim()).ToList();
        }

        public async Task AddClaimsAsync(Usuario user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {

            foreach (var claim in claims)
            {
                string query = "insert into sys.usuario_claim_tb (id_usuario, claim) values (@id_usuario, @claim)";

                await _connection.ExecuteAsync(query,
                    new
                    {
                        id_usuario = user.Id,
                        claim = claim.Value
                    });
            }
        }

        public async Task ReplaceClaimAsync(Usuario user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            var param = new List<Claim> { claim };
            await this.RemoveClaimsAsync(user, param, cancellationToken);


            param = new List<Claim> { newClaim };
            await this.AddClaimsAsync(user, param, cancellationToken);
        }

        public async Task RemoveClaimsAsync(Usuario user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            foreach (var claim in claims)
            {
                string query = "delete from sys.usuario_claim_tb where claim = @claim)";

                await _connection.ExecuteAsync(query,
                    new
                    {
                        claim = claim.Value
                    });
            }
        }

        public Task<IList<Usuario>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            var result = new List<Usuario>();
            return Task.FromResult((IList<Usuario>)result);
        }
    }
}
