using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.DTOs.ContactDetail
{
    public class EditContactDetailDTO
    {
        public long Id { get; set; }
        public long ContactId { get; set; }
        public string ContactNumber { get; set; }
        public long? EditedUserId { get; set; }

    }
}
