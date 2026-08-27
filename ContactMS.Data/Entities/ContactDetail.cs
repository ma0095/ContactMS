using ContactMS.Data.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Data.Entities
{
    public class ContactDetail : IContactDetail
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public long ContactNumber { get; set; }

        public virtual Contact? Contact { get; set; }
    }
}
