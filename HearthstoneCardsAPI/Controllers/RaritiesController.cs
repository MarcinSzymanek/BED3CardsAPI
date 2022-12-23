using HearthstoneCardsAPI.Models;
using HearthstoneCardsAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace HearthstoneCardsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RaritiesController : ControllerBase
    {
        IMongoService _mongoService;
        private ILogger<RaritiesController> _logger;
        public RaritiesController(ILogger<RaritiesController> logger, IMongoService mongoService)
        {
            _logger = logger;
            _mongoService = mongoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Rarity>> GetRarities()
        {
            var collection = _mongoService.Client.GetDatabase("HearthstoneDb").GetCollection<Metadata>("Metadata");
            var m = collection.Find(_ => true).Single();
            var ip = HttpContext.Connection.RemoteIpAddress;
            _logger.Log(LogLevel.Information, "Rarities GET request served for " + ip);
            return Ok(m.Rarities.ToList());
        }
    }
}
