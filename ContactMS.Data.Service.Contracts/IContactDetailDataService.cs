using ContactMS.Data.Contract;
using ContactMS.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Data.Service.Contracts
{
    public interface IContactDetailDataService
    {
        Task<ActionStatus<IContactDetail>> CreateContactDetail(IContactDetail result);
        Task<ActionStatus<List<IContactDetail>>> CreateContactDetails(List<IContactDetail> detailmodel);
        Task<ActionStatus<IContactDetail>> EditContactDetail(IContactDetail result);
        Task<ActionStatus<IContactDetail>> GetContactDetailById(long id);
    }
}
