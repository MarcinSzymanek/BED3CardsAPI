using HearthstoneCardsAPI.Models;
using HearthstoneCardsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace HearthstoneCardsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly ILogger<ClassesController> _logger;
        private readonly IMongoService _mongoService;

        public ClassesController(ILogger<ClassesController> logger, IMongoService mongoService)
        {
            _logger = logger;
            _mongoService = mongoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Rarity>> GetClasses()
        {
            var collection = _mongoService.Client.GetDatabase("HearthstoneDb").GetCollection<Metadata>("Metadata");
            var m = collection.Find(_ => true).Single();
            _logger.Log(LogLevel.Information, "Classes GET request served.");
            return Ok(m.Classes.ToList());
        }
    }
}
