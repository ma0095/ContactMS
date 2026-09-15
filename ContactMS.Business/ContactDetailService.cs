using ContactMS.Business.Contracts;
using ContactMS.Data.Contract;
using ContactMS.Data.Service.Contracts;
using ContactMS.DTOs.Contact;
using ContactMS.DTOs.ContactDetail;
using ContactMS.Framework.Extensions;
using ContactMS.Framework.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Business
{
    public class ContactDetailService : IContactDetailService
    {
        IContactDetailDataService _contactDetailDataService;
        private readonly APIDataMapper<IContactDetail, ContactDetailDTO> _contactDetailMapper;
        private readonly APIDataMapper<IContactDetail, CreateContactDetailDTO> _createContactDetailRequestMapper;
        private readonly APIDataMapper<IContactDetail, EditContactDetailDTO> _editContactDetailRequestMapper;

        public ContactDetailService(IContactDetailDataService contactDetailDataService,
            APIDataMapper<IContactDetail, ContactDetailDTO> contactDetailMapper,
            APIDataMapper<IContactDetail, CreateContactDetailDTO> createContactDetailRequestMapper,
            APIDataMapper<IContactDetail, EditContactDetailDTO> editContactDetailRequestMapper
            )
        {
            _contactDetailDataService = contactDetailDataService;
            _contactDetailMapper = contactDetailMapper;
            _createContactDetailRequestMapper = createContactDetailRequestMapper;
            _editContactDetailRequestMapper = editContactDetailRequestMapper;
        }

        public async Task<ActionStatus<ContactDetailDTO>> CreateContactDetail(CreateContactDetailDTO dto)
        {
            try
            {
                IContactDetail result = _createContactDetailRequestMapper.ToEntity(dto);
                ActionStatus<IContactDetail> data = await _contactDetailDataService.CreateContactDetail(result);
                if (data)
                {
                    ContactDetailDTO response = _contactDetailMapper.ToObject(data.Result);

                    return new ActionStatus<ContactDetailDTO>(true, response);
                }
                else if (data.HasException)
                {
                    return new ActionStatus<ContactDetailDTO>(new ResponseVM("BCCE001"));
                }
                return new ActionStatus<ContactDetailDTO>(data);
            }
            catch (Exception ex)
            {
                return new ActionStatus<ContactDetailDTO>("BPC-CreateContactDetail", ex);
            }
        }
        public async Task<ActionStatus<ContactDetailDTO>> GetContactDetailById(long id)
        {
            try
            {
                ActionStatus<IContactDetail> contact = await _contactDetailDataService.GetContactDetailById(id);
                if (contact)
                {
                    ContactDetailDTO response = _contactDetailMapper.ToObject(contact.Result);
                    return new ActionStatus<ContactDetailDTO>(true, response);
                }
                else if (contact.HasException)
                {
                    return new ActionStatus<ContactDetailDTO>(new ResponseVM("CCGE001"));
                }
                return new ActionStatus<ContactDetailDTO>(contact);
            }
            catch (Exception ex)
            {
                return new ActionStatus<ContactDetailDTO>("BPC-GetContactDetailById", ex);
            }
        }
        public async Task<ActionStatus<ContactDetailDTO>> EditContactDetail(EditContactDetailDTO requestmodel)
        {
            try
            {
                IContactDetail result = _editContactDetailRequestMapper.ToEntity(requestmodel);
                ActionStatus<IContactDetail> data = await _contactDetailDataService.EditContactDetail(result);
                if (data)
                {
                    ContactDetailDTO Response = _contactDetailMapper.ToObject(data.Result);
                    return new ActionStatus<ContactDetailDTO>(true, Response);
                }
                else if (data.HasException)
                {
                    return new ActionStatus<ContactDetailDTO>(new ResponseVM("BCEE001"));
                }
                return new ActionStatus<ContactDetailDTO>(data);
            }
            catch (Exception ex)
            {
                return new ActionStatus<ContactDetailDTO>("BPC-EditContactDetail", ex);
            }
        }


    }
}
