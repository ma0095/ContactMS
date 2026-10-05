using ContactMS.Data.Contract;
using ContactMS.Data.Service.Contracts;
using ContactMS.DTOs;
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
    public class ContactDataService :BaseDataService,IContactDataService
    {
        private IRepository<IContact> _contactRepo;
        private IRepository<IContactDetail> _contactDetailRepo;

        public ContactDataService(IUnitOfWork unitOfWork):base(unitOfWork)
        {
            _contactRepo = unitOfWork.Repository<IContact>();
            _contactDetailRepo = unitOfWork.Repository<IContactDetail>();
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
        public async Task<ActionStatus<List<IContact>>> GetPaginatedContact(PaginationParams pagination)
        {
            try
            {
                int count = 0;
                IQueryable<IContact> resultItems = _contactRepo.Entities;
                count = await resultItems.CountAsync();
                if (count > 0)
                {
                    resultItems = resultItems.OrderByDescending(x => x.Id);
                    List<IContact> result = await resultItems.Skip((pagination.Page - 1) * pagination.PageSize).Take(pagination.PageSize).ToListAsync();
                    return new ActionStatus<List<IContact>>(true, result, count);
                }
                return new ActionStatus<List<IContact>>(new ResponseVM("0000", "No data"));
            }
            catch (Exception ex)
            {
                return new ActionStatus<List<IContact>>("ContactDataService GetPaginatedContact", ex);
            }
        }
        //public async Task<ActionStatus<IContact>> CreateContactWithDetails(IContact contact, List<IContactDetail> details)
        //{
        //    try
        //    {
        //        await using var transaction = await UnitOfWork.BeginTransactionAsync();

        //        IContact createdContact = _contactRepo.Add(contact);
        //        int contactCount = await UnitOfWork.CommitAsync();
        //        if (contactCount <= 0)
        //        {
        //            await transaction.RollbackAsync();
        //            return new ActionStatus<IContact>(new ResponseVM("DCC0001"));
        //        }

        //        details.ForEach(detail =>
        //        {
        //            detail.ContactId = createdContact.Id;
        //            detail.CreatedUserId = createdContact.CreatedUserId;
        //        });

        //        _contactDetailRepo.Insert(details);
        //        //await transaction.RollbackAsync();
        //        //return new ActionStatus<IContact>(new ResponseVM("DCC0001"));
        //        int detailCount = await UnitOfWork.CommitAsync();
        //        if (detailCount <= 0)
        //        {
        //            await transaction.RollbackAsync();
        //            return new ActionStatus<IContact>(new ResponseVM("DCC0001"));
        //        }

        //        await transaction.CommitAsync();
        //        return new ActionStatus<IContact>(true, createdContact);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ActionStatus<IContact>("DPC-CreateContactWithDetails", ex);
        //    }
        //}

    }
}
