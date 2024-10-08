﻿

namespace Ihc.CrackSports.WebApp.Configurations.DependenciasInjection
{
    public static class InjectDependencia
    {
        /// <summary>
        /// Adiciona todas as dependências do projeto
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddInjecaoDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();
            services.AddApplications();
            services.AddServices();
            services.AddCommands();
            services.AddRepositorys();
            services.AddDataBaseConfiguration(configuration);
        }
    }
}
