using Dito.Autocomplete.Infrastructure.Services;
using Dito.Autocomplete.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Dito.Autocomplete.Controllers
{
    [Route("[controller]")]
    public class UserActivityController : Controller
    {
        private readonly IUserActivityService _userActivityService;

        public UserActivityController(IUserActivityService userActivityService)
        {
            _userActivityService = userActivityService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserActivity>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Exception), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserActivity>>> Get()
        {
            try
            {
                return new OkObjectResult(await _userActivityService.GetAll());
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Exception), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post([FromBody] UserActivityRequest request)
        {
            try
            {
                await _userActivityService.Create(request);
                return new OkResult();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
