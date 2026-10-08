using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.DTOs.Hierarchy
{
    public interface IResponse
    {
         string ResponseMessage { get; set; }
         string ResponseCode { get; set; }
    }
}
