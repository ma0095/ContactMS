using ContactMS.Business.Contracts;
using ContactMS.Data.Contract;
using ContactMS.Data.Service.Contracts;
using ContactMS.DTOs.Contact;
using ContactMS.Framework.Extensions;
using ContactMS.Framework.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Business
{
    public class ContactService : IContactService
    {
        IContactDataService _contactDataService;
        private readonly APIDataMapper<IContact, ContactDTO> _contactMapper;
        private readonly APIDataMapper<IContact, CreateContactDTO> _createContactRequestMapper;
        private readonly APIDataMapper<IContact, EditContactDTO> _editContactRequestMapper;

        public ContactService(IContactDataService contactDataService,

            APIDataMapper<IContact, ContactDTO> contactMapper,
            APIDataMapper<IContact, CreateContactDTO> createContactRequestMapper,
            APIDataMapper<IContact, EditContactDTO> editContactRequestMapper

            )
        {
            _contactDataService = contactDataService;
            _contactMapper = contactMapper;
            _createContactRequestMapper = createContactRequestMapper;
            _editContactRequestMapper = editContactRequestMapper;
        }
        public async Task<ActionStatus<ContactDTO>> CreateContact(CreateContactDTO dto)
        {
            try
            {
                IContact result = _createContactRequestMapper.ToEntity(dto);
                ActionStatus<IContact> data = await _contactDataService.CreateContact(result);
                if (data)
                {
                    ContactDTO response = _contactMapper.ToObject(data.Result);

                    return new ActionStatus<ContactDTO>(true, response);
                }
                else if (data.HasException)
                {
                    return new ActionStatus<ContactDTO>(new ResponseVM("BCCE001"));
                }
                return new ActionStatus<ContactDTO>(data);
            }
            catch (Exception ex)
            {
                return new ActionStatus<ContactDTO>("BPC-CreateContact", ex);
            }
        }
        public async Task<ActionStatus<ContactDTO>> GetContactById(long id)
        {
            try
            {
                ActionStatus<IContact> contact = await _contactDataService.GetContactById(id);
                if (contact)
                {
                    ContactDTO response = _contactMapper.ToObject(contact.Result);
                    return new ActionStatus<ContactDTO>(true, response);
                }
                else if (contact.HasException)
                {
                    return new ActionStatus<ContactDTO>(new ResponseVM("CCGE001"));
                }
                return new ActionStatus<ContactDTO>(contact);
            }
            catch (Exception ex)
            {
                return new ActionStatus<ContactDTO>("BPC-GetContactById", ex);
            }
        }
        public async Task<ActionStatus<ContactDTO>> EditContact(EditContactDTO requestmodel)
        {
            try
            {
                IContact result = _editContactRequestMapper.ToEntity(requestmodel);
                ActionStatus<IContact> data = await _contactDataService.EditContact(result);
                if (data)
                {
                    ContactDTO Response = _contactMapper.ToObject(data.Result);
                    return new ActionStatus<ContactDTO>(true, Response);
                }
                else if (data.HasException)
                {
                    return new ActionStatus<ContactDTO>(new ResponseVM("BCEE001"));
                }
                return new ActionStatus<ContactDTO>(data);
            }
            catch (Exception ex)
            {
                return new ActionStatus<ContactDTO>("BPC-EditContact", ex);
            }
        }

    }
}
