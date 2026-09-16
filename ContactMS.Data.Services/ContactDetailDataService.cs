using ContactMS.Data.Contract;
using ContactMS.Data.Service.Contracts;
using ContactMS.Framework.Data;
using ContactMS.Framework.Data.Services;
using ContactMS.Framework.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Data.Services
{
    public class ContactDetailDataService : BaseDataService,IContactDetailDataService
    {
        private IRepository<IContactDetail> _contactDetailRepo;
        public ContactDetailDataService(IUnitOfWork unitOfWork):base(unitOfWork)
        {
            _contactDetailRepo = unitOfWork.Repository<IContactDetail>();
        }
        public async Task<ActionStatus<IContactDetail>> CreateContactDetail(IContactDetail requestdata)
        {
            try
            {
                IContactDetail data = _contactDetailRepo.Add(requestdata);


                int count = await UnitOfWork.CommitAsync();
                if (count > 0)
                {
                    return new ActionStatus<IContactDetail>(true, data);
                }
                return new ActionStatus<IContactDetail>(new ResponseVM("DCC0001"));
            }
            catch (Exception ex)
            {
                return new ActionStatus<IContactDetail>("DPC-CreateContactDetail", ex);
            }
        }
        public async Task<ActionStatus<IContactDetail>> GetContactDetailById(long id)
        {
            try
            {
                IContactDetail data = await _contactDetailRepo.Entities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (data != null)
                {
                    return new ActionStatus<IContactDetail>(true, data);
                }
                return new ActionStatus<IContactDetail>(new ResponseVM("DCG0002"));
            }
            catch (Exception ex)
            {
                return new ActionStatus<IContactDetail>("DPC-GetContactDetailById", ex);
            }
        }
        public async Task<ActionStatus<IContactDetail>> EditContactDetail(IContactDetail entity)
        {
            try
            {
                IContactDetail data = await _contactDetailRepo.Entities.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (data != null)
                {
                    data.ContactNumber = entity.ContactNumber;
                    data.ContactId = entity.ContactId;
                    data.EditedUserId = entity.EditedUserId;
                    _contactDetailRepo.Update(data);
                    int count = await UnitOfWork.CommitAsync();
                    if (count > 0)
                    {
                        return new ActionStatus<IContactDetail>(true, data);
                    }
                    return new ActionStatus<IContactDetail>(new ResponseVM("DPE0001"));
                }
                return new ActionStatus<IContactDetail>((ActionStatus)data);
            }
            catch (Exception ex)
            {
                return new ActionStatus<IContactDetail>("DPC-EditContactDetail", ex);
            }
        }
        public async Task<ActionStatus<List<IContactDetail>>> CreateContactDetails(List<IContactDetail> requestdata)
        {
            try
            {
                _contactDetailRepo.Insert(requestdata);
                int count = await UnitOfWork.CommitAsync();
                if (count > 0)
                {
                    return new ActionStatus<List<IContactDetail>>(true, requestdata);
                }
                return new ActionStatus<List<IContactDetail>>(new ResponseVM("DCC0001"));
            }
            catch (Exception ex)
            {
                return new ActionStatus<List<IContactDetail>>("DPC-CreateContactDetails", ex);
            }
        }

    }
}
