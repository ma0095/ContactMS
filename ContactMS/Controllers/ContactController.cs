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
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [HttpPost]
        [Route("CreateContact")]
        public async Task<IActionResult> CreateContact(CreateContactDTO dto)
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
        public async Task<IActionResult> GetContactById(long id)
        {
            try
            {
                string request = JsonConvert.SerializeObject(id);
                Microsoft.Extensions.Primitives.StringValues accessToken = Request.Headers["Bearer"];
                string token = accessToken.ToString();

                ActionStatus<ContactDTO> responsemodel = await _contactService.GetContactById(id, token);
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
        public async Task<IActionResult> EditContact(EditContactDTO dto)
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
        [HttpPost]
        [Route("CreateContactWithDetails")]
        public async Task<IActionResult> CreateContactWithDetails(CreateContactDTO dto)
        {
            try
            {
                var result = await _contactService.CreateContactWithDetails(dto);
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
        [HttpPost]
        [Route("GetPaginatedContact")]
        public async Task<IActionResult> GetPaginatedContact(PaginationParams pagination)
        {
            try
            {
                ActionStatus<List<ContactDTO>> result = await _contactService.GetPaginatedContact(pagination);
                if (result)
                {
                    return Ok(result);
                }
                else if (result.HasException)
                {
                    return StatusCode(500, new ActionStatus(new ResponseVM("ODGE001")));
                }
                return BadRequest(new ActionStatus(result.Response));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionStatus(new ResponseVM("ODGE001")));
            }
        }
        // MS - MS create test

        [HttpPost]
        [Route("CreateContactWithHierarchy")]
        public async Task<IActionResult> CreateContactWithHierarchy(CreateContactDTO dto)
        {
            try
            {
                string request = JsonConvert.SerializeObject(dto);
                Microsoft.Extensions.Primitives.StringValues accessToken = Request.Headers["Bearer"];
                string token = accessToken.ToString();
                ActionStatus<ContactDTO> result = await _contactService.CreateContactWithHierarchy(dto, token);
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

    }
}
