using Ihc.CrackSports.Core.Authorization.Claims;
using Ihc.CrackSports.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Ihc.CrackSports.WebApp.Controllers
{
	public class ControllerBase : Controller
	{
		private readonly IAlunoService alunoService;
		private readonly IClubService clubService;

		public ControllerBase(IAlunoService alunoService)
		{
			this.alunoService = alunoService;		
		}

		public ControllerBase(IClubService clubService)
		{
			this.clubService = clubService;			
		}

		protected async Task RefreshImageUser(ClaimsPrincipal User)
		{
			if (User.HasImage() && string.IsNullOrEmpty(User.GetImage()))
			{
				if (User.IsAuthenticated())
				{
					if (User.IsAluno())
					{
						var aluno = await this.alunoService.GetByIdUsuario(long.Parse(User.GetIdentificador()));
						Roles.SetImage(aluno.FotoAlunoBase64);
					}
					//else if (User.IsClub())
					//{
					//	var club = await this.clubService.ObterByIdUsuario(long.Parse(User.GetIdentificador()));
					//	Roles.SetImage(club.ImagemBase64);
					//}
				}
			}
		}

		protected bool CanAccess(ClaimsPrincipal User, string role)
		{
			return User?.CanAccess(role) ?? false;
		}

		protected bool CanInsert(ClaimsPrincipal User, string role)
		{
			return User?.CanInsert(role) ?? false;
		}

		protected bool CanUpdate(ClaimsPrincipal User, string role)
		{
			return User?.CanUpdate(role) ?? false;
		}

		protected bool CanDelete(ClaimsPrincipal User, string role)
		{
			return User?.CanDelete(role) ?? false;
		}

		protected bool CanRead(ClaimsPrincipal User, string role)
		{
			return User?.CanRead(role) ?? false;
		}
		protected long GetIdUsuarioLogado()
		{
			var claimUser = HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier);

			if (claimUser != null)
			{
				return long.Parse(claimUser.Value.ToString());
			}
			else
				return -1;
		}

		protected bool IsAluno()
		{
			return User?.IsAluno() ?? false;
		}
		
	}
}
