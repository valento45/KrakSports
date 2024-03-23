using Ihc.CrackSports.Core.Responses.CepService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Services.Interfaces
{
    public interface ICEPService
    {
        Task<CepResponse?> GetEnderecoByCEPAsync(string cep);


    }
}
