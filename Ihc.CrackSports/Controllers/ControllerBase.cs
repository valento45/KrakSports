using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Claims;
using Ihc.CrackSports.Core.Security;
using Ihc.CrackSports.Core.Services;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.WebApp.Models.Usuarios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection;
using System.Security.Claims;
using static Dapper.SqlMapper;

namespace Ihc.CrackSports.WebApp.Controllers
{
	public class ControllerBase : Controller
	{
		private IAlunoService alunoService;
		private IClubService clubService;
		protected readonly UserManager<Usuario> _userManager;


		#region Construtores
		public ControllerBase(IAlunoService alunoService, UserManager<Usuario> userManager)
		{
			this.alunoService = alunoService;
			_userManager = userManager;
		}

		public ControllerBase(IClubService clubService, UserManager<Usuario> userManager)
		{
			this.clubService = clubService;
			_userManager = userManager;
		}

		public ControllerBase(IClubService clubService, IAlunoService alunoService, UserManager<Usuario> userManager)
		{
			this.clubService = clubService;
			this.alunoService = alunoService;
			_userManager = userManager;
		}

		#endregion


		protected async Task RefreshImageUser(ClaimsPrincipal User)
		{
			if (User != null)
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
						else if (User.IsClub())
						{
							var club = await this.clubService.ObterByIdUsuario(long.Parse(User.GetIdentificador()));
							Roles.SetImage(club.ImagemBase64);
						}
					}
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


		protected string GetUserNameLogado()
		{
			var claimUser = HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name);

			if (claimUser != null)
			{
				return claimUser.Value.ToString();
			}
			else
				return "";
		}


		protected string GetPasswordHashLogado()
		{
			var claimUser = HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Hash);

			if (claimUser != null)
			{
				return claimUser.Value.ToString();
			}
			else
				return "";
		}

		protected bool IsAluno()
		{
			return User?.IsAluno() ?? false;
		}

		protected async Task ConfiguraUserAluno(Usuario user, ClaimsIdentity identity)
		{
            var aluno = await alunoService.GetByIdUsuario(user.Id);

            if (!string.IsNullOrEmpty(aluno.FotoAlunoBase64))
            {
                identity.AddClaim(new Claim("Image", ""));
                Roles.SetImage(aluno.FotoAlunoBase64);
            }
        }


        protected async Task ConfiguraUserClub(Usuario user,  ClaimsIdentity identity)
        {
            var club = await clubService.ObterByIdUsuario(user.Id);

            if (!string.IsNullOrEmpty(club.ImagemBase64))
            {
                identity.AddClaim(new Claim("Image", ""));
                Roles.SetImage(club.ImagemBase64);
            }
        }

        protected async Task<bool> Autenticar(LoginModel model)
		{
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, Security.Encrypt(model.PasswordHash)))
                {
                    var identity = new ClaimsIdentity("cookies");

                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Name, model.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.Hash, user.PasswordHash));
                    identity.AddClaims(await _userManager.GetClaimsAsync(user));



                    if (identity.HasClaim(x => x.Value == Roles.ALUNO))                    
						await ConfiguraUserAluno(user, identity);                        
                    
                    else if (identity.HasClaim(x => x.Value == Roles.CLUB))                    
                        await ConfiguraUserClub(user, identity);       
					

                    var userClaim = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync("cookies", userClaim);
                    return true;
                }
			}
			
			return false;
        }

	}
}
