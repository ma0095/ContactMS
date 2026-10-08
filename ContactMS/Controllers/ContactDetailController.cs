using ContactMS.Business;
using ContactMS.Business.Contracts;
using ContactMS.DTOs.Contact;
using ContactMS.DTOs.ContactDetail;
using ContactMS.Framework.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactDetailController : ControllerBase
    {
        private readonly IContactDetailService _contactDetailService;
        private readonly ILogger<ContactDetailController> _logger;

        public ContactDetailController(IContactDetailService contactDetailService, ILogger<ContactDetailController> logger)
        {
            _contactDetailService = contactDetailService;
            _logger = logger;
        }

        [HttpPost]
        [Route("CreateContactDetail")]
        public async Task<IActionResult> CreateContactDetail(CreateContactDetailDTO dto)
        {
            _logger.LogInformation("Creating contact detail for payload: {@Dto}", dto);
            try
            {
                ActionStatus<ContactDetailDTO> result = await _contactDetailService.CreateContactDetail(dto);
                if (result)
                {
                    result.Response = new ResponseVM("CCC0002");
                    _logger.LogInformation("Contact detail created successfully. Id: {ContactDetailId}", result.Result?.Id);
                    return Ok(result);
                }
                else if (result.HasException)
                {
                    _logger.LogError(result.Exception, "Error creating contact detail");
                    return StatusCode(500, new ActionStatus(new ResponseVM("CCCE002")));
                }
                _logger.LogWarning("CreateContactDetail returned bad request: {@Result}", result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception in CreateContactDetail");
                return StatusCode(500, new ActionStatus(new ResponseVM("CCCE002")));
            }
        }

        [HttpGet]
        [Route("GetContactDetailById/{id}")]
        public async Task<IActionResult> GetContactDetailById(long id)
        {
            _logger.LogInformation("Fetching contact detail by Id: {ContactDetailId}", id);
            try
            {
                ActionStatus<ContactDetailDTO> responsemodel = await _contactDetailService.GetContactDetailById(id);
                if (responsemodel)
                {
                    responsemodel.Response = new ResponseVM("CCG0002");
                    _logger.LogInformation("Fetched contact detail successfully for Id: {ContactDetailId}", id);
                    return Ok(responsemodel);
                }
                else if (responsemodel.HasException)
                {
                    _logger.LogError(responsemodel.Exception, "Error fetching contact detail for Id: {ContactDetailId}", id);
                    return StatusCode(500, new ActionStatus(new ResponseVM("CCGE002")));
                }
                _logger.LogWarning("GetContactDetailById returned bad request for Id: {ContactDetailId}", id);
                return BadRequest(responsemodel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception in GetContactDetailById for Id: {ContactDetailId}", id);
                return StatusCode(500, new ActionStatus(new ResponseVM("CCGE002")));
            }
        }

        [HttpPost]
        [Route("EditContactDetail")]
        public async Task<IActionResult> EditContactDetail(EditContactDetailDTO dto)
        {
            _logger.LogInformation("Editing contact detail for Id: {ContactDetailId}", dto?.Id);
            try
            {
                ActionStatus<ContactDetailDTO> responsemodel = await _contactDetailService.EditContactDetail(dto);
                if (responsemodel)
                {
                    responsemodel.Response = new ResponseVM("CCE0003");
                    _logger.LogInformation("Edited contact detail successfully for Id: {ContactDetailId}", dto?.Id);
                    return Ok(responsemodel);
                }
                else if (responsemodel.HasException)
                {
                    _logger.LogError(responsemodel.Exception, "Error editing contact detail for Id: {ContactDetailId}", dto?.Id);
                    return StatusCode(500, new ActionStatus(new ResponseVM("CCEE003")));
                }
                _logger.LogWarning("EditContactDetail returned bad request for payload: {@Dto}", dto);
                return BadRequest(responsemodel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception in EditContactDetail for Id: {ContactDetailId}", dto?.Id);
                return StatusCode(500, new ActionStatus(new ResponseVM("CCEE003")));
            }
        }
    }
}
