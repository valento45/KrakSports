﻿using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Claims;
using Ihc.CrackSports.Core.Authorization.Context.Interfaces;
using Ihc.CrackSports.Core.Commands.Interfaces;
using Ihc.CrackSports.Core.Extensions;
using Ihc.CrackSports.Core.Notifications.Hubs;
using Ihc.CrackSports.Core.Objetos.Alunos;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.WebApp.Application.Interfaces;
using Ihc.CrackSports.WebApp.Models.Alunos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ihc.CrackSports.WebApp.Controllers
{
    public class AlunoController : ControllerBase
    {


        public AlunoController(IAlunoService alunoService, IClubService clubService, UserManager<Usuario> userManager, INotificationCommand notificationCommand, IUsuarioContext httpContextAccessor, IMessageApplication messageApplication) : base(clubService, alunoService, userManager, notificationCommand, httpContextAccessor, messageApplication)
        {

        }



        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DadosAluno(long idAluno)
        {
            var aluno = await _alunoService.GetById(idAluno);

            if (!(base.GetIdUsuarioLogado() == aluno.IdUsuario|| User.IsAdm()))
                throw new Exception("Não é possível prosseguir pois você não está autorizado.");


            await base.RefreshImageUser(User);



            DadosAlunoViewModel obj = new DadosAlunoViewModel
            {
                DadosAluno = await _alunoService.GetById(idAluno),
                Clubs = await _clubService.ObterByNome(string.Empty, 0) ?? new List<Core.Objetos.Clube.Club>()
            };

            return View(obj);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DadosAluno(DadosAlunoViewModel request)
        {
            var aluno = await _alunoService.GetById(request.DadosAluno.Id);

            if (!(base.GetIdUsuarioLogado() == aluno?.IdUsuario || User.IsAdm()))
                throw new Exception("Não é possível prosseguir pois você não está autorizado.");


            request.Clubs = await _clubService.ObterByNome(string.Empty, 0);

            if (request.File != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    await request.File.CopyToAsync(ms);
                    request.DadosAluno.FotoAlunoBase64 = Convert.ToBase64String(ms.ToArray());
                }
                HttpContext.Session.SetString("photo", request.DadosAluno.FotoAlunoBase64);

            }

            var response = await _alunoService.UpdateDadosGerais(request.DadosAluno);

            if (response.IsSuccessStatusCode)
            {
                request.DadosAluno = await _alunoService.GetById(request.DadosAluno.Id);

                return View(request);
            }
            else
                throw new HttpRequestException($"Status Code: {response.StatusCode}\r\nMensagem: {response.Message}");

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DadosGerais(long idAluno)
        {

            if (User != null)
            {
                await base.RefreshImageUser(User);
            }

            DadosAlunoViewModel obj = new DadosAlunoViewModel
            {
                DadosAluno = await _alunoService.GetById(idAluno)
            };


            return View(obj);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DadosGerais(DadosAlunoViewModel dadosAlunoViewModel)
        {

            var response = await _alunoService.UpdateResponsavelEndereco(dadosAlunoViewModel.DadosAluno);

            if (response.IsSuccessStatusCode)
                return View(dadosAlunoViewModel);
            else
                throw new Exception($"{response.StatusCode} - {response.Message}");
        }
    }
}
