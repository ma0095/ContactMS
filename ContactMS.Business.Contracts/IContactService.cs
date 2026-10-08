using ContactMS.DTOs;
using ContactMS.DTOs.Contact;
using ContactMS.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Business.Contracts
{
    public interface IContactService
    {
        Task<ActionStatus<ContactDTO>> CreateContact(CreateContactDTO dto);
        Task<ActionStatus<ContactDTO>> CreateContactWithDetails(CreateContactDTO dto);
        Task<ActionStatus<ContactDTO>> CreateContactWithHierarchy(CreateContactDTO dto, string token);
        Task<ActionStatus<ContactDTO>> EditContact(EditContactDTO dto);
        Task<ActionStatus<ContactDTO>> GetContactById(long id, string token);
        Task<ActionStatus<List<ContactDTO>>> GetPaginatedContact(PaginationParams pagination);
    }
}
