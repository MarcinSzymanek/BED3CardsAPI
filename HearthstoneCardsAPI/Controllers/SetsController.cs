using HearthstoneCardsAPI.Models;
using HearthstoneCardsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace HearthstoneCardsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SetsController : ControllerBase
    {
        IMongoService _mongoService;
        private ILogger<SetsController> _logger;
        public SetsController(ILogger<SetsController> logger, IMongoService mongoService)
        {
            _logger = logger;
            _mongoService = mongoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Rarity>> GetSets()
        {
            var collection = _mongoService.Client.GetDatabase("HearthstoneDb").GetCollection<Metadata>("Metadata");
            var m = collection.Find(_ => true).Single();
            var ip = HttpContext.Connection.RemoteIpAddress;
            _logger.Log(LogLevel.Information, "Sets GET request served for " + ip);
            return Ok(m.Sets.ToList());
        }
    }
}

