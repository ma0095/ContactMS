using ContactMS.DTOs.ContactDetail;
using ContactMS.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Business.Contracts
{
    public interface IContactDetailService
    {
        Task<ActionStatus<ContactDetailDTO>> CreateContactDetail(CreateContactDetailDTO dto);
        Task<ActionStatus<ContactDetailDTO>> EditContactDetail(EditContactDetailDTO dto);
        Task<ActionStatus<ContactDetailDTO>> GetContactDetailById(long id);
    }
}
