using Ihc.CrackSports.Core.Extensions;
using Ihc.CrackSports.Core.Responses.CepService;
using Ihc.CrackSports.Core.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Services
{
    public class CEPService : ICEPService
    {

        public HttpClient _client { get; set; }
        private HttpResponseMessage _response;

        public CEPService()
        {
            _client = new HttpClient();

            _client.BaseAddress = new Uri("https://viacep.com.br");
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }




        public async Task<CepResponse?> GetEnderecoByCEPAsync(string cep)
        {
            try
            {
                var response = await _client.GetAsync(CreateRequest(EndPointCEPService.Cep, cep, "json"));

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<CepResponse>(await response.Content.ReadAsStringAsync());
                    return result;
                }

            }
            catch { }
            return null;
        }


        private string CreateRequest(EndPointCEPService endpoint, string value, string type)
        {
            string result = "";

            if (endpoint == EndPointCEPService.Cep)
            {
                result = $"{endpoint.GetEnumDescription()}/{value}/{type}";
            }
            return result;
        }
    }
}
