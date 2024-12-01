using Ihc.CrackSports.Core.Objetos.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Authorization
{
    public class Usuario : IdentityUser<long>
    {
        public IEnumerable<Claim> Claims { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

        public string ImagemBase64 { get; set; }
        public Usuario() : base()
        {
            Claims = new List<Claim>();
        }

        public Usuario(string userName) :  base(userName)
        {
            Claims = new List<Claim>();
        }



        public bool CheckPassword(string password)
        {

            return PasswordHash == password;    
        }




        public Usuario Copy()
        {
            return new Usuario
            {
                AccessFailedCount = AccessFailedCount,
                Claims = Claims,
                ConcurrencyStamp = ConcurrencyStamp,
                Email = Email,
                EmailConfirmed = EmailConfirmed,
                Id = Id,
                ImagemBase64 = ImagemBase64,
                LockoutEnabled = LockoutEnabled,
                LockoutEnd = LockoutEnd,
                PasswordHash = PasswordHash,
                NormalizedEmail = NormalizedEmail,
                NormalizedUserName = NormalizedUserName,
                PhoneNumber = PhoneNumber,
                PhoneNumberConfirmed = PhoneNumberConfirmed,
                SecurityStamp = SecurityStamp,
                TipoUsuario = TipoUsuario,
                TwoFactorEnabled = TwoFactorEnabled,
                UserName = UserName
                
            };
        }
    }
}
