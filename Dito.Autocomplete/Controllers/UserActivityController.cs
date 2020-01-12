using Dito.Autocomplete.Infrastructure.Repositories;
using Dito.Autocomplete.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dito.Autocomplete.Controllers
{
    [Route("[controller]")]
    public class UserActivityController : Controller
    {
        private readonly IUserActivityRepository _repo;

        public UserActivityController(IUserActivityRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await _repo.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserActivity userActivity)
        {
            await _repo.Create(userActivity);
            return new OkResult();
        }
    }
}
