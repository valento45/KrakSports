using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Base.Auxiliar;
using Ihc.CrackSports.Core.Objetos.Competicoes;
using Ihc.CrackSports.Core.Responses.AgendaEventos;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.WebApp.Application;
using Ihc.CrackSports.WebApp.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ihc.CrackSports.WebApp.Controllers;

public class PlacarController : ControllerBase
{
    private readonly IEventoApplication _eventoApplication;

    public PlacarController(IClubService clubService, IAlunoService alunoService, UserManager<Usuario> userManager, INotificationCommand notificationCommand, IUsuarioContext httpContextAccessor,
        IMessageApplication messageApplication, IEventoApplication eventoApplication) : base(clubService, alunoService, userManager, notificationCommand, httpContextAccessor, messageApplication)
    {
        _eventoApplication = eventoApplication;
    }





    [HttpGet]
    public async Task<IActionResult> Index(DateTime de, DateTime ate)
    {
        de = de <= new DateTime() ? DateTime.Now.AddDays(-30) : de;
        ate = ate <= new DateTime() ? DateTime.Now : ate;


        Periodo periodo = new Periodo(de, ate);

        var eventos = await _eventoApplication.GetEventos(periodo.De, periodo.Ate);

        foreach (var evento in eventos.Eventos)
        {
            evento.Clube1.InformarAtletas(await _alunoService.ObterAlunosPorClub(evento.Clube1.Id));
            evento.Clube2.InformarAtletas(await _alunoService.ObterAlunosPorClub(evento.Clube2.Id));


            var placarAtlteas = await _eventoApplication.ObterPlacar(evento.IdEvento);

            evento.InformarAtletasClubeUm(placarAtlteas.Where(x => x.IdClube == evento.Clube1.Id).ToList());
            evento.InformarAtletasClubeDois(placarAtlteas.Where(x => x.IdClube == evento.Clube2.Id).ToList());
        }


        return View(eventos);
    }



    [HttpPost]
    public async Task<IActionResult> FiltrarPlacar(Periodo periodo)
    {       

        var eventos = await _eventoApplication.GetEventos(periodo.De, periodo.Ate);

        foreach (var evento in eventos.Eventos)
        {
            evento.Clube1.InformarAtletas(await _alunoService.ObterAlunosPorClub(evento.Clube1.Id));
            evento.Clube2.InformarAtletas(await _alunoService.ObterAlunosPorClub(evento.Clube2.Id));


            var placarAtlteas = await _eventoApplication.ObterPlacar(evento.IdEvento);

            evento.InformarAtletasClubeUm(placarAtlteas.Where(x => x.IdClube == evento.Clube1.Id).ToList());
            evento.InformarAtletasClubeDois(placarAtlteas.Where(x => x.IdClube == evento.Clube2.Id).ToList());
        }


        return View(nameof(Index), eventos);
    }


}
