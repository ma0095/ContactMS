using ContactMS.Business.Contracts;
using ContactMS.DTOs.Contact;
using ContactMS.Framework.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [HttpPost]
        [Route("CreateContact")]
        public async Task<ActionResult> CreateContact(CreateContactDTO dto)
        {
            try
            {
                ActionStatus<ContactDTO> result = await _contactService.CreateContact(dto);
                if (result)
                {
                    result.Response = new ResponseVM("CCC0001");
                    return Ok(result);
                }
                else if (result.HasException)
                {
                    return StatusCode(500, new ActionStatus(new ResponseVM("CCCE001")));
                }
                return BadRequest(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionStatus(new ResponseVM("CCCE001")));
            }
        }
        [HttpGet]
        [Route("GetContactById/{id}")]
        public async Task<ActionResult> GetContactById(long id)
        {
            try
            {
                ActionStatus<ContactDTO> responsemodel = await _contactService.GetContactById(id);
                if (responsemodel)
                {
                    responsemodel.Response = new ResponseVM("CCG0001");

                    return Ok(responsemodel);
                }
                else if (responsemodel.HasException)
                {

                    return StatusCode(500, new ActionStatus(new ResponseVM("CCGE001")));
                }
                return BadRequest(responsemodel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionStatus(new ResponseVM("CCGE001")));
            }
        }
        [HttpPost]
        [Route("EditContact")]
        public async Task<ActionResult> EditContact(EditContactDTO dto)
        {
            try
            {
                ActionStatus<ContactDTO> responsemodel = await _contactService.EditContact(dto);
                if (responsemodel)
                {
                    responsemodel.Response = new ResponseVM("CCE0001");
                    return Ok(responsemodel);
                }
                else if (responsemodel.HasException)
                {
                    return StatusCode(500, new ActionStatus(new ResponseVM("CCEE001")));
                }
                return BadRequest(responsemodel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionStatus(new ResponseVM("CCEE001")));
            }
        }

    }
}
