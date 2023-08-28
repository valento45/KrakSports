using Ihc.CrackSports.Core.Authorization;
using Ihc.CrackSports.Core.Authorization.Claims;
using Ihc.CrackSports.Core.Security;
using Ihc.CrackSports.Core.Services.Interfaces;
using Ihc.CrackSports.WebApp.Models.Clube;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ihc.CrackSports.WebApp.Controllers
{
    public class ClubController : ControllerBase
    {
        private readonly IClubService _clubService;

        private readonly IUsuarioService _usuarioService;

        public ClubController(IClubService clubService, UserManager<Usuario> user, IUsuarioService usuarioService) : base(clubService, user)
        {
            _clubService = clubService;
            _usuarioService = usuarioService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await base.RefreshImageUser(User);
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Cadastro(long idUsuario)
        {
            if (idUsuario <= 0)
                throw new ArgumentNullException("Usuário inválido !");

            await base.RefreshImageUser(User);

            ClubViewModel model = new ClubViewModel();

            model.DadosClub = await _clubService.ObterByIdUsuario(idUsuario) ?? throw new ArgumentNullException("Usuário inválido !");
            model.DadosUsuario = await _usuarioService.GetById(idUsuario);

            return View("Partial/CadastroClubPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(ClubViewModel model)
        {
            if (model != null)
            {

                if (!model.isInsert())
                {
                    if (model.DadosUsuario.Id != long.Parse(User.GetIdentificador()))
                        if (!base.CanUpdate(User, Roles.CLUB))
                            return View("Unauthorized");
                }

                else
                {
                    var user = await _userManager.FindByNameAsync(model.DadosUsuario.UserName);

                    if (user == null)
                    {
                        user = new Usuario()
                        {
                            UserName = model.DadosUsuario.UserName,
                            PasswordHash = model.DadosUsuario.PasswordHash,
                            Email = model.DadosUsuario.Email,
                            NormalizedUserName = model.DadosUsuario.UserName.ToUpper(),
                            TipoUsuario = Core.Objetos.Enums.TipoUsuario.Club
                        };

                        var resultUser = await _userManager.CreateAsync(user, Security.Encrypt(user.PasswordHash));

                        if (resultUser.Succeeded)
                        {
                            user = await _userManager.FindByNameAsync(model.DadosUsuario.UserName);
                            await _userManager.AddClaimsAsync(user, new List<Claim> { new Claim(ClaimTypes.Role, Roles.CLUB) });
                            model.DadosClub.IdUsuario = user.Id;
                        }
                    }
                    else
                        throw new Exception("Usuário informado já existe !");
                }

                if (model.File != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        await model.File.CopyToAsync(ms);
                        model.DadosClub.ImagemBase64 = Convert.ToBase64String(ms.ToArray());
                        Roles.SetImage(model.DadosClub.ImagemBase64);
                        
                    }
                }

                var result = await _clubService.Salvar(model.DadosClub);

                if (result.IsSuccessStatusCode)
                    return View("Partial/CadastroClubPartial", model);
                else
                    throw new Exception($"Erro ao salvar dados do club. Codigo {result.StatusCode} - {result.Message}");

            }

            return View("Partial/CadastroClubPartial", model);
        }
    }
}
