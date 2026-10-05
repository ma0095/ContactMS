using ContactMS.DTOs.ContactDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.DTOs.Contact
{
    public class ContactDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int ActiveStatus { get; set; }
        public long? CreatedUserId { get; set; }
        public long? EditedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }

        public List<ContactDetailDTO>? ContactDetails { get; set; }

    }
}
