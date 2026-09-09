using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.DTOs.ContactDetail
{
    public class ContactDetailDTO
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public long ContactNumber { get; set; }
    }
}
