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

        public Usuario() : base()
        {
            Claims = new List<Claim>();
        }

        public Usuario(string userName) :  base(userName)
        {
            Claims = new List<Claim>();
        }


    }
}
