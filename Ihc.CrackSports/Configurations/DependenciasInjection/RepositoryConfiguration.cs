﻿
using Ihc.CrackSports.Core.Repositorys;
using Ihc.CrackSports.Core.Repositorys.Interfaces;
using Newtonsoft.Json;
using Npgsql;
using System.Data;
using System.Diagnostics;

namespace Ihc.CrackSports.WebApp.Configurations.DependenciasInjection
{
    public static class RepositoryConfiguration
    {

        public static void AddRepositorys(this IServiceCollection services)
        {
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IAlunoRepository, AlunoRepository>();
            services.AddTransient<IResponsavelRepository, ResponsavelRepository>();
        }


        public static void AddDataBaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = "";

            //if (!Debugger.IsAttached)
            //{
            //    string encryptBase = configuration.GetConnectionString("Production");

            //    var baseDecrypt = Security.Decrypt(encryptBase);
            //    var basedados = JsonConvert.DeserializeObject<DatabaseSecurity>(baseDecrypt);

            //    connectionString = $"Server={basedados.Server};Database={basedados.Database};user={basedados.User};password={basedados.Pass};SslMode=VerifyFull;";
            //}

            //else
            connectionString = configuration.GetConnectionString("Postgres");


            NpgsqlConnection con = new NpgsqlConnection(connectionString);

            services.AddSingleton<IDbConnection>(con);
        }

    }
}