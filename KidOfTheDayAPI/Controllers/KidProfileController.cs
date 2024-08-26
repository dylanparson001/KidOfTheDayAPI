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
    //[Authorize]
    public class KidProfileController : ControllerBase
    {
        private readonly IKidProfileRepository _kidProfileRepository;

        public KidProfileController(IKidProfileRepository kidProfileRepository)
        {
            _kidProfileRepository = kidProfileRepository;
        }


        [HttpPost]
        [Route("addkidprofile")]
        public async Task<IActionResult> AddKidProfile(KidProfileDto kidProfileDto)
        {
            if (kidProfileDto == null)
            {
                return BadRequest();
            }

            await _kidProfileRepository.AddKidProfile(kidProfileDto);

            return NoContent();
        }


        [HttpGet]
        [Route("getkidsbyuserid")]
        public async Task<ActionResult<List<KidProfile>>> GetKidsByUserId(int userId)
        {
            var result = await _kidProfileRepository.GetKidsByUser(userId);

            if (result == null) 
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("getkidbyid")]
        public async Task<ActionResult<KidProfile>> GetKidById(int id)
        {

            if (id == 0 || id < 0)
            {
                return BadRequest("Valid kid not provided");

            }
            var result = await _kidProfileRepository.GetKidProfileById(id);

            if (result == null )
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpPut]
        [Route("updatekidschedule")]
        public async Task<ActionResult> UpdateKidProfile(int id, int schedule)
        {
            if (id == 0 || id < 0)
            {
                return BadRequest("Valid Id not sent");
            }

            if (schedule < 0) 
            {
                return BadRequest("Schedule cannot be lower than 0");
            }

            var kidProfile = await _kidProfileRepository.GetKidProfileById(id);

            if (kidProfile == null)
            {
                return BadRequest("Kid profile does not exist");
            }


            await _kidProfileRepository.UpdateKidProfile(id, schedule);

            return NoContent();

        }

        [HttpDelete]
        [Route("deletekidprofile")]
        public async Task<IActionResult> DeleteKidProfile(int kidId)
        {
            await _kidProfileRepository.DeleteKidProfile(kidId);

            return NoContent();
        }

    }
}
