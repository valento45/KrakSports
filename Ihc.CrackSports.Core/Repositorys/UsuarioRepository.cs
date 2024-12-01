using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Repositorys.Base;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ihc.CrackSports.Core.Security;
using Ihc.CrackSports.Core.Requests.Aluno;

namespace Ihc.CrackSports.Core.Repositorys
{
    public class UsuarioRepository : RepositoryBase, IUsuarioRepository
    {
        public UsuarioRepository(IDbConnection connection) : base(connection)
        { }

        public async Task<bool> Incluir(CadastroRequest request)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("insert into sys.usuario_tb (login, senha, tipo, email) values (@login, @senha, @tipo, @email) returning id_usuario;");

            cmd.Parameters.AddWithValue(@"login", request.Login);
            cmd.Parameters.AddWithValue(@"senha", Security.Security.Encrypt(request.Senha));
            cmd.Parameters.AddWithValue(@"tipo", (int)request.Tipo);
            cmd.Parameters.AddWithValue(@"email", request.Email ?? "");


            var result = await base.ExecuteScalarAsync(cmd);

            int id;
            if (int.TryParse(result?.ToString(), out id))
            {
                request.IdAluno = id;
                return true;
            }
            else
                return false;

        }


        public async Task<bool> Atualizar(CadastroRequest request)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("update sys.usuario_tb set login = @login, senha = @senha, tipo = @tipo, email= @email where id_usuario = @id_usuario");

            cmd.Parameters.AddWithValue(@"login", request.Login);
            cmd.Parameters.AddWithValue(@"senha", request.Senha);
            cmd.Parameters.AddWithValue(@"tipo", (int)request.Tipo);
            cmd.Parameters.AddWithValue(@"email", request.Email);
            cmd.Parameters.AddWithValue(@"id_usuario", request.IdUsuario);


            var result = await base.ExecuteCommand(cmd);

            return result;
        }

        public async Task<bool> Excluir(long id)
        {

            string query = $"delete from sys.usuario_claim_tb where id_usuario = {id}";

            if (await base.ExecuteAsync(query))
                return await base.ExecuteAsync($"delete from sys.usuario_tb where id_usuario = {id}");

            return false;

        }

        public async Task<string> GetMessage()
        {
            return base.GetMessage();
        }

        public async Task<Usuario?> ObterPorUserName(string userName)
        {
            string query = $"select id_usuario as Id, login as UserName, senha as PasswordHash, email as Email from sys.usuario_tb where UPPER(login) LIKE '{userName}'";

            var obj = await base.QueryAsync<Usuario>(query);

            if (obj != null)
                return obj.FirstOrDefault();
            else
                return null;
        }

        public async Task<Usuario> GetById(long id)
        {
            string query = $"select id_usuario as Id, login as UserName, senha as PasswordHash, email as Email from sys.usuario_tb where id_usuario  = @id_usuario";

            var obj = await base.QueryAsync<Usuario>(query, new { id_usuario = id });

            if (obj != null)
                return obj.FirstOrDefault();
            else
                return null;
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            string query = $"select id_usuario as Id, login as UserName, senha as PasswordHash, email as Email " +
                $"from sys.usuario_tb";

            return await base.QueryAsync<Usuario>(query) ?? new List<Usuario>();


        }

        public async Task<IEnumerable<Usuario>> GetAllAdministradores()
        {
            string query = $"select us.id_usuario as Id, us.login as UserName, us.senha as PasswordHash, us.email as Email, us.login, cl.claim from sys.usuario_tb as us " +
                $" inner join sys.usuario_claim_tb as cl on us.id_usuario = cl.id_usuario " +
                $" where cl.claim like 'adm' ";

            return await base.QueryAsync<Usuario>(query) ?? new List<Usuario>();
        }

        public async Task<bool> AtualizarUsuario(Usuario usuario)
        {
            string query = "UPDATE sys.usuario_tb" +
                $" SET " +
                $" senha = '{Ihc.CrackSports.Core.Security.Security.Encrypt(usuario.PasswordHash)}'" +
                $" WHERE id_usuario = {usuario.Id}";




            return await base.ExecuteAsync(query);

        }
    }
}
