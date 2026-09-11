using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.DTOs.Contact
{
    public class EditContactDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int ActiveStatus { get; set; }
        public long? EditedUserId { get; set; }
    }
}
