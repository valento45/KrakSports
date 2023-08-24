﻿
using Ihc.CrackSports.Core.Services;
using Ihc.CrackSports.Core.Services.Interfaces;
using System.Globalization;
using System.Text;

namespace Ihc.CrackSports.WebApp.Configurations.DependenciasInjection
{
    public static class ServiceConfiguration
    {

        public static void AddServices(this IServiceCollection services)
        {          

            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IAlunoService, AlunoService>();

          
        }
        
    }
}
