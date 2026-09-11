using ContactMS.Data.Contract;
using ContactMS.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Data.Service.Contracts
{
    public interface IContactDataService
    {
        Task<ActionStatus<IContact>> CreateContact(IContact result);
        Task<ActionStatus<IContact>> EditContact(IContact result);
        Task<ActionStatus<IContact>> GetContactById(long id);
    }
}
