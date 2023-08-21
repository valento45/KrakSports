using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Authorization.Claims
{
    public class ClaimDto
    {
        public long id_usuario { get; set; }
        public string claim { get; set; }

        public Claim ToClaim()
        {
            return new Claim(ClaimTypes.Role, claim);
        }
    }
}
