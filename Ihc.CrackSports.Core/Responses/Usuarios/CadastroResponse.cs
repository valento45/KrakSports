using Ihc.CrackSports.Core.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Responses.Usuarios
{
    public class CadastroResponse : ResponseBase
    {

        public long Id { get; set; }
        public string TipoCadastro { get; set; }
        public bool IsInsert { get; set; }
        public CadastroResponse()
        {
            
        }
    }
}
