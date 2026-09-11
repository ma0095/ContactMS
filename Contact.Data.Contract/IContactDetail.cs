using ContactMS.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Data.Contract
{
    public interface IContactDetail : IAuditable, IEntity
    {
        public long ContactId { get; set; }
        public string ContactNumber { get; set; }
    }
}
