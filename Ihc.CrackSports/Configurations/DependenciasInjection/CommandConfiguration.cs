﻿

using Ihc.CrackSports.Core.Commands;
using Ihc.CrackSports.Core.Commands.Interfaces;

namespace Ihc.CrackSports.WebApp.Configurations.DependenciasInjection
{
    public static class CommandConfiguration
    {
        public static void AddCommands(this IServiceCollection services)
        {
            services.AddTransient<IUsuarioCommand, UsuarioCommand>();            
            services.AddTransient<IAlunoCommand, AlunoCommand>();            
            services.AddTransient<IResponsavelCommand, ResponsavelCommand>();            
            services.AddTransient<ICadastroAlunoCommand, CadastroAlunoCommand>();            
             
        }
    }
}