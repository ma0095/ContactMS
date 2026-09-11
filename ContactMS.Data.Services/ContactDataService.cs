using ContactMS.Data.Contract;
using ContactMS.Data.Service.Contracts;
using ContactMS.Framework.Data;
using ContactMS.Framework.Extensions;
using Microsoft.EntityFrameworkCore;
using ProductMS.Framework.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Data.Services
{
    public class ContactDataService :BaseDataService,IContactDataService
    {
        private IRepository<IContact> _contactRepo;
        public ContactDataService(IUnitOfWork unitOfWork):base(unitOfWork)
        {
            _contactRepo = unitOfWork.Repository<IContact>();
        }
        public async Task<ActionStatus<IContact>> CreateContact(IContact requestdata)
        {
            try
            {
                IContact data = _contactRepo.Add(requestdata);


                int count = await UnitOfWork.CommitAsync();
                if (count > 0)
                {
                    return new ActionStatus<IContact>(true, data);
                }
                return new ActionStatus<IContact>(new ResponseVM("DCC0001"));
            }
            catch (Exception ex)
            {
                return new ActionStatus<IContact>("DPC-CreateContact", ex);
            }
        }
        public async Task<ActionStatus<IContact>> GetContactById(long id)
        {
            try
            {
                IContact data = await _contactRepo.Entities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (data != null)
                {
                    return new ActionStatus<IContact>(true, data);
                }
                return new ActionStatus<IContact>(new ResponseVM("DCG0002"));
            }
            catch (Exception ex)
            {
                return new ActionStatus<IContact>("DPC-GetContactById", ex);
            }
        }
        public async Task<ActionStatus<IContact>> EditContact(IContact entity)
        {
            try
            {
                IContact data = await _contactRepo.Entities.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (data != null)
                {
                    data.Name = entity.Name;
                    data.ActiveStatus = entity.ActiveStatus;
                    data.EditedUserId = entity.EditedUserId;
                    _contactRepo.Update(data);
                    int count = await UnitOfWork.CommitAsync();
                    if (count > 0)
                    {
                        return new ActionStatus<IContact>(true, data);
                    }
                    return new ActionStatus<IContact>(new ResponseVM("DPE0001"));
                }
                return new ActionStatus<IContact>((ActionStatus)data);
            }
            catch (Exception ex)
            {
                return new ActionStatus<IContact>("DPC-EditContact", ex);
            }
        }

    }
}
