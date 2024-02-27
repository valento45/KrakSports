using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Objetos.Competicoes;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.WebApp.Application;
using Ihc.CrackSports.WebApp.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

namespace Ihc.CrackSports.WebApp.Controllers;

public class PlacarController : ControllerBase
{

    public PlacarController(IClubService clubService, IAlunoService alunoService, UserManager<Usuario> userManager, INotificationCommand notificationCommand, IUsuarioContext httpContextAccessor, 
        IMessageApplication messageApplication) : base(clubService, alunoService, userManager, notificationCommand, httpContextAccessor, messageApplication)
    {
    }

    [HttpGet]
    public IActionResult Index()
    {

 
        /////Pegar o metodo GetEventos por data

        //var eventosList = await _eventoApplication.GetEventos()


        //    ///Obter os atletas dos eventos 
        //      eventosList.Clube1.InformarAtletas(await _alunoService.ObterAlunosPorClub(eventosList.Clube1.Id));
        //eventosList.Clube2.InformarAtletas(await _alunoService.ObterAlunosPorClub(eventosList.Clube2.Id));



        /////Obter e preencher o placar dos eventos 
        //foreach () {
        //    var placarAtlteas = await _eventoApplication.ObterPlacar(idEvento);
        //    evento.InformarAtletasClubeUm(placarAtlteas.Where(x => x.IdClube == evento.Clube1.Id).ToList());
        //    evento.InformarAtletasClubeDois(placarAtlteas.Where(x => x.IdClube == evento.Clube2.Id).ToList());

        //}
        return View();
    }

    


}
