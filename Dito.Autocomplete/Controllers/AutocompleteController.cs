using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Dito.Autocomplete.Infrastructure.Services;
using Dito.Autocomplete.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dito.Autocomplete.Controllers
{
    [Route("[controller]")]
    public class AutocompleteController : Controller
    {
        private readonly IAutocompleteService _autocompleteService;

        public AutocompleteController(IAutocompleteService autocompleteService)
        {
            _autocompleteService = autocompleteService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserActivity>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Exception), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserActivityResponse>>> Get(string term)
        {
            try
            {
                return new OkObjectResult(await _autocompleteService.GetByTerm(term));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}