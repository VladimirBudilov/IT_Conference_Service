using IT_Conference_Service.Data.Entitiess;
using IT_Conference_Service.Services.Interfaces;
using IT_Conference_Service.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace IT_Conference_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationsController : ControllerBase
    {
        public readonly IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> GetApplication(Guid id)
        {
            var application = await _applicationService.GetApplication(id);
            return Ok(application);
        }

        [HttpGet]
        public async Task< ActionResult<IEnumerable<Application>>> GetApplications([FromQuery] DateTime? submittedAfter, [FromQuery] DateTime? unsubmittedOlder)
        {
            IEnumerable<ApplicationModel> applications = new List<ApplicationModel>();
            if(submittedAfter != null && unsubmittedOlder == null)
            {
                applications = await _applicationService.GetAllAfterData(submittedAfter.Value);
                return Ok(applications);
            }
            else if(unsubmittedOlder != null && submittedAfter == null)
            {
                applications = await _applicationService.GetAllUnsubmittedAfterData(unsubmittedOlder.Value);
                return Ok(applications);
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpPost]
        public ActionResult<Application> CreateApplication([FromBody] Application application)
        {
            // Implement your logic to create an application here
            return CreatedAtAction(nameof(GetApplication), new { id = application.Id }, application);
        }

        [HttpPut("{id}")]
        public ActionResult<Application> UpdateApplication(Guid id, [FromBody] Application application)
        {
            // Implement your logic to update an application here
            return Ok(application);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteApplication(Guid id)
        {
            // Implement your logic to delete an application here
            return Ok();
        }

        [HttpPost("{id}/submit")]
        public IActionResult SubmitApplication(Guid id)
        {
            // Implement your logic to submit an application here
            return Ok();
        }

        [HttpGet("users/{userId}/currentapplication")]
        public ActionResult<Application> GetCurrentApplicationForUser(Guid userId)
        {
            // Implement your logic to get the current application for a user here
            return Ok(new Application());
        }

        [HttpGet("activities")]
        public ActionResult<IEnumerable<Activity>> GetActivities()
        {
            // Implement your logic to get the list of possible activities here
            return Ok(new List<Activity>());
        }
    }
}
