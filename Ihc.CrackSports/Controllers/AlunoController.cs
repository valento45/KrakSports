using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Claims;
using Ihc.CrackSports.Core.Extensions;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.WebApp.Models.Alunos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ihc.CrackSports.WebApp.Controllers
{
	public class AlunoController : ControllerBase
	{
		private readonly IAlunoService _alunoService;
		private readonly IClubService _clubService;

		public AlunoController(IAlunoService alunoService, IClubService clubService, UserManager<Usuario> userManager) : base(alunoService, userManager)
		{
			_alunoService = alunoService;
			_clubService = clubService;
		}




		[HttpGet]
		public async Task<IActionResult> DadosAluno(long idAluno)
		{

			await base.RefreshImageUser(User);


			DadosAlunoViewModel obj = new DadosAlunoViewModel
			{
				DadosAluno = await _alunoService.GetById(idAluno),
				Clubs = await _clubService.ObterByNome(string.Empty, 0) ?? new List<Core.Objetos.Clube.Club>()
			};

			return View(obj);
		}

		[HttpPost]
		public async Task<IActionResult> DadosAluno(DadosAlunoViewModel request)
		{
			request.Clubs = await _clubService.ObterByNome(string.Empty, 0);

			if (request.File != null)
			{
				using (MemoryStream ms = new MemoryStream())
				{
					await request.File.CopyToAsync(ms);
					request.DadosAluno.FotoAlunoBase64 = Convert.ToBase64String(ms.ToArray());
				}
				Roles.SetImage(request.DadosAluno.FotoAlunoBase64);			

			}

			var response = await _alunoService.UpdateDadosGerais(request.DadosAluno);

			if (response.IsSuccessStatusCode)
			{			
				return View(request);
			}
			else
				throw new HttpRequestException($"Status Code: {response.StatusCode}\r\nMensagem: {response.Message}");

		}

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
