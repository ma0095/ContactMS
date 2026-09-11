using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.DTOs.ContactDetail
{
    public class CreateContactDetailDTO
    {
        public long ContactId { get; set; }
        public string ContactNumber { get; set; }
        public long? CreatedUserId { get; set; }

    }
}
