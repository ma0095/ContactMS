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
        IContactDetailService _contactDetailService;
        public ContactDetailController(IContactDetailService contactDetailService)
        {
            _contactDetailService = contactDetailService;
        }
        [HttpPost]
        [Route("CreateContactDetail")]
        public async Task<IActionResult> CreateContactDetail(CreateContactDetailDTO dto)
        {
            try
            {
                ActionStatus<ContactDetailDTO> result = await _contactDetailService.CreateContactDetail(dto);
                if (result)
                {
                    result.Response = new ResponseVM("CCC0002");
                    return Ok(result);
                }
                else if (result.HasException)
                {
                    return StatusCode(500, new ActionStatus(new ResponseVM("CCCE002")));
                }
                return BadRequest(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionStatus(new ResponseVM("CCCE002")));
            }
        }
        [HttpGet]
        [Route("GetContactDetailById/{id}")]
        public async Task<IActionResult> GetContactDetailById(long id)
        {
            try
            {
                ActionStatus<ContactDetailDTO> responsemodel = await _contactDetailService.GetContactDetailById(id);
                if (responsemodel)
                {
                    responsemodel.Response = new ResponseVM("CCG0002");

                    return Ok(responsemodel);
                }
                else if (responsemodel.HasException)
                {

                    return StatusCode(500, new ActionStatus(new ResponseVM("CCGE002")));
                }
                return BadRequest(responsemodel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionStatus(new ResponseVM("CCGE002")));
            }
        }
        [HttpPost]
        [Route("EditContactDetail")]
        public async Task<IActionResult> EditContactDetail(EditContactDetailDTO dto)
        {
            try
            {
                ActionStatus<ContactDetailDTO> responsemodel = await _contactDetailService.EditContactDetail(dto);
                if (responsemodel)
                {
                    responsemodel.Response = new ResponseVM("CCE0003");
                    return Ok(responsemodel);
                }
                else if (responsemodel.HasException)
                {
                    return StatusCode(500, new ActionStatus(new ResponseVM("CCEE003")));
                }
                return BadRequest(responsemodel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionStatus(new ResponseVM("CCEE003")));
            }
        }
    }
}
