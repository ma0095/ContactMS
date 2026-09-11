using ContactMS.Data.Contract;
using ContactMS.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Data.Entities
{
    public class Contact : BaseEntity,IContact
    {
        public string Name { get ; set ; }
        public int ActiveStatus { get ; set; }
        public long? CreatedUserId { get; set; }
        public long? EditedUserId { get; set; }

        public virtual ICollection<ContactDetail>? ContactDetails { get; set; }
    }
}
