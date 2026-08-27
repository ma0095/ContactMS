using ContactMS.Data.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Data.Entities
{
    public class Contact : IContact
    {
        public int Id { get ; set ; }
        public string Name { get ; set ; }
        public int ActiveStatus { get ; set; }

        public virtual ICollection<ContactDetail>? ContactDetails { get; set; }
    }
}
