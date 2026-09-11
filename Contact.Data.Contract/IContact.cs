using ContactMS.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Data.Contract
{
    public interface IContact : IAuditable, IEntity
    {
        string Name { get; set; }
        int ActiveStatus { get; set; }
    }
}
