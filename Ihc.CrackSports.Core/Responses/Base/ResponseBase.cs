using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ihc.CrackSports.Core.Responses.Base
{
    public abstract class ResponseBase
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsSuccessStatusCode
        {
            get
            {
                return StatusCode == 200;
            }
        }
    }
}
