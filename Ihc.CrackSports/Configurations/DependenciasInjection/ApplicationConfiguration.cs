

using Ihc.CrackSports.Core.Commands;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.WebApp.Application;
using Ihc.CrackSports.WebApp.Application.Interfaces;

namespace Ihc.CrackSports.WebApp.Configurations.DependenciasInjection
{
    public static class ApplicationConfiguration
    {
        public static void AddApplications(this IServiceCollection services)
        {
            services.AddTransient<IAlunoApplication, AlunoApplication>();            
            services.AddTransient<IClubApplication, ClubApplication>();            
            services.AddTransient<IEventoApplication, EventoApplication>();            
           
             
        }
    }
}
