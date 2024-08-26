using KidOfTheDayAPI.Dtos;
using KidOfTheDayAPI.Interfaces;
using KidOfTheDayAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KidOfTheDayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ResponsibilityController : ControllerBase
    {
        private readonly IResponsibiltiesRepository _responsibiltiesRepository;

        public ResponsibilityController(IResponsibiltiesRepository responsibiltiesRepository)
        {
            _responsibiltiesRepository = responsibiltiesRepository;
        }
        [HttpGet]
        [Route("getresponsibilitybykid")]
        public async Task<ActionResult<List<Responsibility>>> GetResponsibilitiesByKid(int kidId)
        {
            if (kidId == 0 || kidId < 0)
            {
                return BadRequest("Valid Kid ID not sent");
            }

            var result = await _responsibiltiesRepository.GetKidsResponsibilities(kidId);


            return Ok(result);  
        }

        [HttpPost]
        [Route("addresponsibilitytokid")]
        public async Task<ActionResult> AddResponsibilityToKid(ResponsibilityDto responsibilityDto)
        {
            if (responsibilityDto == null)
            {
                return BadRequest("Value sent is null");
            }

            await _responsibiltiesRepository.AddResponsibility(responsibilityDto);

            return NoContent();
        }
        [HttpDelete]
        [Route("deleteresponsibility")]
        public async Task<ActionResult> DeleteResponsibility(int id)
        {
            if (_responsibiltiesRepository == null)
            {
                return BadRequest("Not a valid responisbility");
            }

            await _responsibiltiesRepository.DeleteResponsibility(id);
            return NoContent();
        }

    }
}
