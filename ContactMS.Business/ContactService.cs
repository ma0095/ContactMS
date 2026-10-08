using ContactMS.Business.Contracts;
using ContactMS.Data.Contract;
using ContactMS.Data.Service.Contracts;
using ContactMS.DTOs;
using ContactMS.DTOs.Contact;
using ContactMS.DTOs.ContactDetail;
using ContactMS.DTOs.Hierarchy;
using ContactMS.Framework.Extensions;
using ContactMS.Framework.Mappers;
using ContactMS.Service.External.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Business
{
    public class ContactService : IContactService
    {
        private readonly IContactDataService _contactDataService;
        private readonly IContactDetailDataService _contactDetailDataService;
        private readonly IHierarchyService _hierarchyService;
        private readonly ILogger<ContactService> _logger;

        private readonly APIDataMapper<IContact, ContactDTO> _contactMapper;
        private readonly APIDataMapper<IContact, CreateContactDTO> _createContactRequestMapper;
        private readonly APIDataMapper<IContact, EditContactDTO> _editContactRequestMapper;
        private readonly APIDataMapper<IContactDetail, ContactDetailDTO> _contactDetailMapper;
        private readonly APIDataMapper<IContactDetail, ContactDetailCreateDTO> _contactDetailCreateMapper;

        public ContactService(IContactDataService contactDataService,
                    IContactDetailDataService contactDetailDataService,
                    IHierarchyService hierarchyService,
                    ILogger<ContactService> logger,
            APIDataMapper<IContact, ContactDTO> contactMapper,
            APIDataMapper<IContact, CreateContactDTO> createContactRequestMapper,
            APIDataMapper<IContact, EditContactDTO> editContactRequestMapper,

            //APIDataMapper<IContactDetail, CreateContactDetailDTO> createContactDetailRequestMapper,
            APIDataMapper<IContactDetail, ContactDetailDTO> contactDetailMapper,
            APIDataMapper<IContactDetail, ContactDetailCreateDTO> contactDetailCreateMapper

            )
        {
            _contactDataService = contactDataService;
            _contactDetailDataService = contactDetailDataService;
            _hierarchyService = hierarchyService;
            _logger = logger;

            _contactMapper = contactMapper;
            _createContactRequestMapper = createContactRequestMapper;
            _editContactRequestMapper = editContactRequestMapper;

            //_createContactDetailRequestMapper = createContactDetailRequestMapper;
            _contactDetailMapper = contactDetailMapper;
            _contactDetailCreateMapper = contactDetailCreateMapper;
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
                _logger.LogError(ex, "Exception in ContactService.CreateContact");
                return new ActionStatus<ContactDTO>("BPC-CreateContact", ex);
            }
        }
        public async Task<ActionStatus<ContactDTO>> GetContactById(long id,string token)
        {
            try
            {
                ActionStatus<HierarchyDTO> account = await _hierarchyService.GetHierarchyDetails(token, id);
                if (!account)
                {
                    return new ActionStatus<ContactDTO>(account);
                }
                else
                {
                    if (account.Result != null)
                    {
                        if (!account.Result.ActiveStatus.Equals(1))
                        {
                            return new ActionStatus<ContactDTO>(new ResponseVM("HMS0001"));
                        }
                    }
                }

                ActionStatus<IContact> contact = await _contactDataService.GetContactById(id);
                if (contact)
                {
                    ContactDTO response = _contactMapper.ToObject(contact.Result);
                    ActionStatus<List<IContactDetail>> contactDetails = await _contactDetailDataService.GetContactDetailsByContactId(id);
                    if(contactDetails != null)
                    {
                        List<ContactDetailDTO> contactDetailsResponse = _contactDetailMapper.ToObjects(contactDetails.Result).ToList();
                        response.ContactDetails = contactDetailsResponse;
                    }
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
                _logger.LogError(ex, "Exception in ContactService.GetContactById for Id {Id}", id);
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
                _logger.LogError(ex, "Exception in ContactService.EditContact for Id {Id}", requestmodel?.Id);
                return new ActionStatus<ContactDTO>("BPC-EditContact", ex);
            }
        }
        public async Task<ActionStatus<ContactDTO>> CreateContactWithDetails(CreateContactDTO dto)
        {
            try
            {
                IContact result = _createContactRequestMapper.ToEntity(dto);
                ActionStatus<IContact> resultEntity = await _contactDataService.CreateContact(result);
                if (resultEntity)
                {
                    ContactDTO resultData = _contactMapper.ToObject(resultEntity.Result);
                    List<IContactDetail> detailmodel = _contactDetailCreateMapper.ToEntities(dto.ContactDetails).ToList();
                    detailmodel.ForEach(x =>
                    {
                        x.ContactId = resultEntity.Result.Id;
                        x.CreatedUserId = resultEntity.Result.CreatedUserId;
                    });
                    ActionStatus<List<IContactDetail>> detailResultEntity = await _contactDetailDataService.CreateContactDetails(detailmodel);
                    if (detailResultEntity)
                    {
                        List<ContactDetailDTO> detailresult = _contactDetailMapper.ToObjects(detailResultEntity.Result).ToList();
                        resultData.ContactDetails = detailresult;
                    }
                    return new ActionStatus<ContactDTO>(true, resultData);
                }
                else if (resultEntity.HasException)
                {
                    return new ActionStatus<ContactDTO>(new ResponseVM("BCCE001"));
                }

                return new ActionStatus<ContactDTO>(resultEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in ContactService.CreateContactWithDetails");
                return new ActionStatus<ContactDTO>("BPC-CreateContactWithDetails", ex);
            }
        }
        public async Task<ActionStatus<List<ContactDTO>>> GetPaginatedContact(PaginationParams pagination)
        {
            try
            {
                ActionStatus<List<IContact>> response = await _contactDataService.GetPaginatedContact(pagination);
                if (response)
                {
                    List<ContactDTO> contacts = _contactMapper.ToObjects(response.Result).ToList();
                    List<long> ids = contacts.Select(x => x.Id).ToList();
                    ActionStatus<List<IContactDetail>> contactDetails = await _contactDetailDataService.GetContactDetailsByContactIds(ids);
                    if (contactDetails != null)
                    {
                        List<ContactDetailDTO> contactDetailsResponse = _contactDetailMapper.ToObjects(contactDetails.Result).ToList();
                        foreach (var contact in contacts)
                        {
                            contact.ContactDetails = contactDetailsResponse.Where(detail => detail.ContactId == contact.Id).ToList();
                        }
                    }
                    return new ActionStatus<List<ContactDTO>>(true, contacts, response.TotalCount);
                }
                else
                {
                    return new ActionStatus<List<ContactDTO>>(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in ContactService.GetPaginatedContact");
                return new ActionStatus<List<ContactDTO>>("ContactService GetPaginatedContact", ex);
            }
        }


        public async Task<ActionStatus<ContactDTO>> CreateContactWithHierarchy(CreateContactDTO dto, string token)
        {
            try
            {
                ActionStatus<IHierarchyResponse> hierarchyResponse = await _hierarchyService.CreateHierarchy(token, dto.Hierarchy);
                if (!hierarchyResponse)
                {
                    return new ActionStatus<ContactDTO>(hierarchyResponse);
                }


                IContact result = _createContactRequestMapper.ToEntity(dto);
                ActionStatus<IContact> data = await _contactDataService.CreateContact(result);
                if (data)
                {
                    ContactDTO response = _contactMapper.ToObject(data.Result);
                    response.Hierarchy = (HierarchyResponse?)hierarchyResponse.Result;
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
                _logger.LogError(ex, "Exception in ContactService.CreateContactWithHierarchy");
                return new ActionStatus<ContactDTO>("BPC-CreateContact", ex);
            }
        }

        //public async Task<ActionStatus<ContactDTO>> CreateContactWithDetails(CreateContactDTO dto)
        //{
        //    try
        //    {
        //        IContact result = _createContactRequestMapper.ToEntity(dto);
        //        List<IContactDetail> detailmodel = _contactDetailCreateMapper.ToEntities(dto.ContactDetails).ToList();

        //        ActionStatus<IContact> resultEntity = await _contactDataService.CreateContactWithDetails(result, detailmodel);

        //        if (resultEntity.HasException)
        //        {
        //            return new ActionStatus<ContactDTO>(new ResponseVM("BCCE001"));
        //        }

        //        return new ActionStatus<ContactDTO>(resultEntity);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ActionStatus<ContactDTO>("BPC-CreateContactWithDetails", ex);
        //    }
        //}

    }
}
