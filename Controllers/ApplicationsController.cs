using IT_Conference_Service.Data.Entitiess;
using IT_Conference_Service.Filters;
using IT_Conference_Service.Services.Interfaces;
using IT_Conference_Service.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IT_Conference_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ExceptionsFilter]
    public class ApplicationsController : ControllerBase
    {
        public readonly IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationModel>> GetApplication(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("The id must not be empty.");

            var application = await _applicationService.GetApplication(id);
            return Ok(application);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationModel>>> GetApplications([FromQuery] DateTime? submittedAfter, [FromQuery] DateTime? unsubmittedOlder)
        {
            if (submittedAfter != null && unsubmittedOlder != null) return BadRequest("You can't use both parameters at the same time. Please use only one of them.");

            IEnumerable<ApplicationModel> applications = new List<ApplicationModel>();
            if (submittedAfter != null)
            {
                applications = await _applicationService.GetAllAfterData(submittedAfter.Value);
            }
            else if (unsubmittedOlder != null)
            {
                applications = await _applicationService.GetAllUnsubmittedAfterData(unsubmittedOlder.Value);
            }

            return Ok(applications);
        }

        [HttpPost]
        public async Task<ActionResult<ApplicationModel>> CreateApplication([FromBody] ApplicationModel application)
        {
            if (!ModelState.IsValid) return BadRequest("The model is not valid.");

            var model = await _applicationService.CreateApplication(application);
            return CreatedAtAction(nameof(GetApplication), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Application>> UpdateApplication(Guid id, [FromBody] ApplicationModel application)
        {
            if (!ModelState.IsValid) return BadRequest("The model is not valid.");

            var model = await _applicationService.UpdateApplication(id, application);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(Guid id)
        {
            await _applicationService.DeleteApplication(id);
            return Ok();
        }

        [HttpPost("{id}/submit")]
        public async Task<IActionResult> SubmitApplication(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("The id must not be empty.");

            await _applicationService.SendApplicationOnReview(id);
            return Ok();
        }

        [HttpGet("users/{userId}/currentapplication")]
        public async Task<ActionResult<Application>> GetCurrentApplicationForAuthor(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("The id must not be empty.");

            var application = await _applicationService.GetUnsubmittedApplication(id);
            return Ok(application);
        }

        [HttpGet("activities")]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
        {
            var activities = await _applicationService.GetActivities();
            return Ok(activities);
        }
    }
}
