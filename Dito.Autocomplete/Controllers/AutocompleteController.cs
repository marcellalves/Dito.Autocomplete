using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Dito.Autocomplete.Infrastructure.Services;
using Dito.Autocomplete.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dito.Autocomplete.Controllers
{
    public class AutocompleteController : Controller
    {
        private readonly IUserActivityService _userActivityService;

        public AutocompleteController(IUserActivityService userActivityService)
        {
            _userActivityService = userActivityService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserActivity>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Exception), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserActivity>>> Get(string term)
        {
            try
            {
                return new OkObjectResult(await _userActivityService.GetByTerm(term));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}