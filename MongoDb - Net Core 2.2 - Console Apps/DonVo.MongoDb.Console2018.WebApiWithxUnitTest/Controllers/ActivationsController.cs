using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Services;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Models.Activations;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Controllers
{
    [Route("api/[controller]")]
    public class ActivationsController : Controller
    {
        private readonly IActivationsService _activationsService;

        public ActivationsController(IActivationsService activationsService)
        {
            _activationsService = activationsService ?? throw new ArgumentNullException(nameof(activationsService));
        }

        [HttpGet(Name = "Get All Activations")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Activation))]
        public async Task<IActionResult> GetManyAsync()
        {
            var allActivations = await _activationsService.ReadAllAsync();

            return Ok(allActivations);
        }

        [HttpGet("{activationId}", Name = "Get Activation by Id")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Activation))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOneByIdAsync(string activationId)
        {
            var activation = await _activationsService.ReadOneAsync(activationId);

            if (activation == null)
                return NotFound();

            return Ok(activation);
        }

        [HttpPost(Name = "Post Activation")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Activation))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostOneAsync([FromBody] PostActivationDto activationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            var postedActivation = await _activationsService.CreateOneAsync(activationDto.ToActivation());

            if (postedActivation == null)
                return BadRequest();

            return StatusCode(201, postedActivation);
        }

        [HttpDelete("{activationId}", Name = "Delete Activation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteOneByIdAsync(string activationId)
        {
            await _activationsService.DeleteOneAsync(activationId);

            return NoContent();
        }
    }
}
