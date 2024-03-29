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
    //[ExceptionsFilter]
    public class ApplicationsController : ControllerBase
    {
        public readonly IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// Retrieves a specific application by unique id.
        /// </summary>
        /// <param name="id">The unique identifier of the application</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationModel>> GetApplication(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("The id must not be empty.");

            var application = await _applicationService.GetApplication(id);
            return Ok(application);
        }

        /// <summary>
        /// Retrieves all applications, optionally filtered by submission date.
        /// </summary>
        /// <param name="submittedAfter">Optional parameter to filter applications submitted after this date</param>
        /// <param name="unsubmittedOlder">Optional parameter to filter applications unsubmitted older than this date</param>
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
                applications = await _applicationService.GetAllUnsubmittedBeforeData(unsubmittedOlder.Value);
            }
            else
            {
                applications = await _applicationService.GetApplications();
            }

            return Ok(applications);
        }

        /// <summary>
        /// Creates a new application.
        /// </summary>
        /// <param name="application">The application model to create</param>
        [HttpPost]
        public async Task<ActionResult<ApplicationModel>> CreateApplication([FromBody] ApplicationModel application)
        {
            if (!ModelState.IsValid) return BadRequest("The model is not valid.");

            var model = await _applicationService.CreateApplication(application);
            return CreatedAtAction(nameof(GetApplication), new { id = model.Id }, model);
        }

        /// <summary>
        /// Updates an existing application.
        /// </summary>
        /// <param name="id">The unique identifier of the application to update</param>
        /// <param name="application">The updated application model</param>
        [HttpPut("{id}")]
        public async Task<ActionResult<Application>> UpdateApplication(Guid id, [FromBody] ApplicationModel application)
        {
            if (!ModelState.IsValid) return BadRequest("The model is not valid.");

            var model = await _applicationService.UpdateApplication(id, application);
            return Ok(model);
        }

        /// <summary>
        /// Deletes an application.
        /// </summary>
        /// <param name="id">The unique identifier of the application to delete</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(Guid id)
        {
            await _applicationService.DeleteApplication(id);
            return Ok();
        }

        /// <summary>
        /// Submits an application for review.
        /// </summary>
        /// <param name="id">The unique identifier of the application to submit</param>
        [HttpPost("{id}/submit")]
        public async Task<IActionResult> SubmitApplication(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("The id must not be empty.");

            await _applicationService.SendApplicationOnReview(id);
            return Ok();
        }

        /// <summary>
        /// Retrieves the current application for a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user</param>
        /// <returns>Returns the current application for the specified user</returns>
        [HttpGet("users/{userId}/currentapplication")]
        public async Task<ActionResult<Application>> GetCurrentApplicationForAuthor(Guid userId)
        {
            if (userId == Guid.Empty) return BadRequest("The id must not be empty.");

            var application = await _applicationService.GetUnsubmittedApplication(userId);
            return Ok(application);
        }

        /// <summary>
        /// Retrieves all activities.
        /// </summary>
        [HttpGet("activities")]
        public async Task<ActionResult<IEnumerable<ActivityModel>>> GetActivities()
        {
            var activities = await _applicationService.GetActivities();
            return Ok(activities);
        }
    }
}
