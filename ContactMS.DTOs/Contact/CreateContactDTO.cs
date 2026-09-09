using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.DTOs.Contact
{
    public class CreateContactDTO
    {
        public string Name { get; set; }
        public int ActiveStatus { get; set; }
    }
}
