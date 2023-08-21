using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Extensions
{
    public static class ExtensionMethods
    {

        public static string FormataCPF(this long cpf)
        {
            return cpf.ToString(@"000\.000\.000\-00");
        }

        public static string FormataCNPJ(this long cnpj)
        {
            return cnpj.ToString(@"00\.000\.000\/0000\-00");
        }
    }
}
