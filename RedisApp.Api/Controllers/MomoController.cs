using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedisApp.Api.Models;
using RedisApp.Api.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedisApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MomoController : ControllerBase
    {

        private readonly ILogger<MomoController> _logger;

        public MomoController(ILogger<MomoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Hi");
            return Ok(new { message = "Hi" });
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<MomoModel>> GetMomo(int id, [FromServices] IMomo momo)
        {
            return await momo.GetMomoAU(id);
        }
    }
}
