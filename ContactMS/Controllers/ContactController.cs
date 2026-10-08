using ContactMS.Business.Contracts;
using ContactMS.DTOs;
using ContactMS.DTOs.Contact;
using ContactMS.Framework.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ContactMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IContactService contactService, ILogger<ContactController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        [HttpPost]
        [Route("CreateContact")]
        public async Task<IActionResult> CreateContact(CreateContactDTO dto)
        {
            _logger.LogInformation("Creating contact for payload: {@Dto}", dto);
            try
            {
                ActionStatus<ContactDTO> result = await _contactService.CreateContact(dto);
                if (result)
                {
                    result.Response = new ResponseVM("CCC0001");
                    _logger.LogInformation("Contact created successfully with Id: {ContactId}", result.Result?.Id);
                    return Ok(result);
                }
                else if (result.HasException)
                {
                    _logger.LogError("Error occurred while creating contact: {@Response}", result);
                    return StatusCode(500, new ActionStatus(new ResponseVM("CCCE001")));
                }
                _logger.LogWarning("Failed to create contact: {@Result}", result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception in CreateContact");
                return StatusCode(500, new ActionStatus(new ResponseVM("CCCE001")));
            }
        }

        [HttpGet]
        [Route("GetContactById/{id}")]
        public async Task<IActionResult> GetContactById(long id)
        {
            _logger.LogInformation("Fetching contact by Id: {ContactId}", id);
            try
            {
                Microsoft.Extensions.Primitives.StringValues accessToken = Request.Headers["Bearer"];
                string token = accessToken.ToString();

                ActionStatus<ContactDTO> responsemodel = await _contactService.GetContactById(id, token);
                if (responsemodel)
                {
                    responsemodel.Response = new ResponseVM("CCG0001");
                    _logger.LogInformation("Contact fetched successfully for Id: {ContactId},result :{@Result}", id, responsemodel);
                    return Ok(responsemodel);
                }
                else if (responsemodel.HasException)
                {
                    _logger.LogError("Error fetching contact by Id: {ContactId},response : {@response}", id, responsemodel);
                    return StatusCode(500, new ActionStatus(new ResponseVM("CCGE001")));
                }
                _logger.LogWarning("GetContactById returned bad request for Id: {ContactId}", id);
                return BadRequest(responsemodel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception in GetContactById for Id: {ContactId}", id);
                return StatusCode(500, new ActionStatus(new ResponseVM("CCGE001")));
            }
        }

        [HttpPost]
        [Route("EditContact")]
        public async Task<IActionResult> EditContact(EditContactDTO dto)
        {
            _logger.LogInformation("Editing contact with Id: {ContactId}", dto?.Id);
            try
            {
                ActionStatus<ContactDTO> responsemodel = await _contactService.EditContact(dto);
                if (responsemodel)
                {
                    responsemodel.Response = new ResponseVM("CCE0001");
                    _logger.LogInformation("Contact edited successfully for Id: {ContactId}", dto?.Id);
                    return Ok(responsemodel);
                }
                else if (responsemodel.HasException)
                {
                    _logger.LogError("Error editing contact for Id: {ContactId},Exception : {@response}", dto?.Id, responsemodel.Exception);
                    return StatusCode(500, new ActionStatus(new ResponseVM("CCEE001")));
                }
                _logger.LogWarning("EditContact returned bad request for payload: {@Dto}", dto);
                return BadRequest(responsemodel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception in EditContact for Id: {ContactId}", dto?.Id);
                return StatusCode(500, new ActionStatus(new ResponseVM("CCEE001")));
            }
        }

        [HttpPost]
        [Route("CreateContactWithDetails")]
        public async Task<IActionResult> CreateContactWithDetails(CreateContactDTO dto)
        {
            _logger.LogInformation("Creating contact with details: {@Dto}", dto);
            try
            {
                var result = await _contactService.CreateContactWithDetails(dto);
                if (result)
                {
                    result.Response = new ResponseVM("CCC0001");
                    _logger.LogInformation("Contact created with details successfully. Id: {ContactId}", result.Result?.Id);
                    return Ok(result);
                }
                else if (result.HasException)
                {
                    _logger.LogError("Error creating contact with details: {@Result}", result);
                    return StatusCode(500, new ActionStatus(new ResponseVM("CCCE001")));
                }

                _logger.LogWarning("CreateContactWithDetails returned bad request: {@Result}", result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception in CreateContactWithDetails");
                return StatusCode(500, new ActionStatus(new ResponseVM("CCCE001")));
            }
        }

        [HttpPost]
        [Route("GetPaginatedContact")]
        public async Task<IActionResult> GetPaginatedContact(PaginationParams pagination)
        {
            _logger.LogInformation("Fetching paginated contacts. Page: {Page}, PageSize: {PageSize}", pagination?.Page, pagination?.PageSize);
            try
            {
                ActionStatus<List<ContactDTO>> result = await _contactService.GetPaginatedContact(pagination);
                if (result)
                {
                    _logger.LogInformation("Retrieved {Count} contacts for page {Page}", result.Result?.Count ?? 0, pagination?.Page);
                    return Ok(result);
                }
                else if (result.HasException)
                {
                    _logger.LogError("Error fetching paginated contacts: {@Result}", result);
                    return StatusCode(500, new ActionStatus(new ResponseVM("ODGE001")));
                }
                _logger.LogWarning("GetPaginatedContact returned bad request");
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception in GetPaginatedContact");
                return StatusCode(500, new ActionStatus(new ResponseVM("ODGE001")));
            }
        }

        [HttpPost]
        [Route("CreateContactWithHierarchy")]
        public async Task<IActionResult> CreateContactWithHierarchy(CreateContactDTO dto)
        {
            _logger.LogInformation("Creating contact with hierarchy: {@Dto}", dto);
            try
            {
                Microsoft.Extensions.Primitives.StringValues accessToken = Request.Headers["Bearer"];
                string token = accessToken.ToString();
                ActionStatus<ContactDTO> result = await _contactService.CreateContactWithHierarchy(dto, token);
                if (result)
                {
                    result.Response = new ResponseVM("CCC0001");
                    _logger.LogInformation("Contact created with hierarchy successfully. Id: {ContactId}", result.Result?.Id);
                    return Ok(result);
                }
                else if (result.HasException)
                {
                    _logger.LogError("Error creating contact with hierarchy: {@Result}", result);
                    return StatusCode(500, new ActionStatus(new ResponseVM("CCCE001")));
                }
                _logger.LogWarning("CreateContactWithHierarchy returned bad request");
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception in CreateContactWithHierarchy");
                return StatusCode(500, new ActionStatus(new ResponseVM("CCCE001")));
            }
        }
    }
}
