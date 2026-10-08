using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.DTOs.Hierarchy
{
    public class Response : IResponse
    {
        public string ResponseMessage { get; set; }
        public string ResponseCode { get; set; }
    }
    
}
