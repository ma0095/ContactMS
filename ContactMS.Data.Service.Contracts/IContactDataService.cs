using ContactMS.Data.Contract;
using ContactMS.DTOs;
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
        //Task<ActionStatus<IContact>> CreateContactWithDetails(IContact result, List<IContactDetail> detailmodel);
        Task<ActionStatus<IContact>> EditContact(IContact result);
        Task<ActionStatus<IContact>> GetContactById(long id);
        Task<ActionStatus<List<IContact>>> GetPaginatedContact(PaginationParams pagination);
    }
}
