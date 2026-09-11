using ContactMS.Data.Contract;
using ContactMS.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Data.Entities
{
    public class ContactDetail : BaseEntity,IContactDetail
    {
        public long ContactId { get; set; }
        public string ContactNumber { get; set; } = string.Empty;
        public long? CreatedUserId { get; set; }
        public long? EditedUserId { get; set; }

        public virtual Contact? Contact { get; set; }
    }
}
