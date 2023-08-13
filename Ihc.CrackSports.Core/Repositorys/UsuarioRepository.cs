using Ihc.CrackSports.Core.Repositorys.Base;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using Ihc.CrackSports.Core.Requests;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Repositorys
{
    public class UsuarioRepository : RepositoryBase, IUsuarioRepository
    {
        public UsuarioRepository(IDbConnection connection) : base(connection)
        { }

        public async Task<bool> Incluir(CadastroRequest request)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("insert into sys.usuario_tb (login, senha, tipo, email) values (@login, @senha, @tipo, @email);");

            cmd.Parameters.AddWithValue(@"login", request.Login);
            cmd.Parameters.AddWithValue(@"senha", request.Senha);
            cmd.Parameters.AddWithValue(@"tipo", (int)request.Tipo);
            cmd.Parameters.AddWithValue(@"email", request.Email);


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


            var result = await base.ExecuteAsync(cmd.CommandText, cmd.Parameters);

            return result;
        }

        public async Task<bool> Excluir(long id)
        {
            string query = $"delete from sys.usuario_tb where id_usuario = {id}";
            return await base.ExecuteAsync(query);
        }

        public async Task<string> GetMessage()
        {
            return base.GetMessage();
        }
    }
}
