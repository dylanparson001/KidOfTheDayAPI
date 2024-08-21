using KidOfTheDayAPI.Dtos;
using KidOfTheDayAPI.Interfaces;
using KidOfTheDayAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KidOfTheDayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

            return Ok(result);
        }
    }
}
