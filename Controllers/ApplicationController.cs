using IT_Conference_Service.Data.Entitiess;
using IT_Conference_Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace IT_Conference_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationController : ControllerBase
    {
        public readonly IApplicaionService _applicationService;

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

        [HttpGet]
        public ActionResult<IEnumerable<Application>> GetApplications([FromQuery] DateTime? submittedAfter, [FromQuery] DateTime? unsubmittedOlder)
        {
            // Implement your logic to get applications based on the query parameters here
            return Ok(new List<Application>());
        }

        [HttpGet("users/{userId}/currentapplication")]
        public ActionResult<Application> GetCurrentApplicationForUser(Guid userId)
        {
            // Implement your logic to get the current application for a user here
            return Ok(new Application());
        }

        [HttpGet("{id}")]
        public ActionResult<Application> GetApplication(Guid id)
        {
            // Implement your logic to get an application by id here
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
