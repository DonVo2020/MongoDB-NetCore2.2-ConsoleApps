using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Services;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Controllers
{
    [Route("api/[controller]")]
    public class MusclesController : Controller
    {
        private readonly IMusclesService _musclesService;

        public MusclesController(IMusclesService musclesService)
        {
            _musclesService = musclesService ?? throw new ArgumentNullException(nameof(musclesService));
        }

        [HttpGet(Name = "Get All Muscles")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Muscle))]
        public async Task<IActionResult> GetManyAsync()
        {
            var allMuscles = await _musclesService.ReadAllAsync();

            return Ok(allMuscles);
        }

        [HttpGet("{muscleId}", Name = "Get Muscle by Id")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Muscle))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOneByIdAsync(string muscleId)
        {
            var muscle = await _musclesService.ReadOneAsync(muscleId);

            if (muscle == null)
                return NotFound();

            return Ok(muscle);
        }

        [HttpPost(Name = "Post Muscle")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Muscle))]
        public async Task<IActionResult> PostOneAsync([FromBody] PostMuscleDto muscleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            var postedMuscle = await _musclesService.CreateOneAsync(muscleDto.ToMuscle());

            if (postedMuscle == null)
                return BadRequest();

            return StatusCode(201, postedMuscle);
        }

        [HttpDelete("{muscleId}", Name = "Delete Muscle")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteOneByIdAsync(string muscleId)
        {
            await _musclesService.DeleteOneAsync(muscleId);

            return NoContent();
        }
    }
}
