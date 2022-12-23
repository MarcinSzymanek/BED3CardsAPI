using HearthstoneCardsAPI.Models;
using HearthstoneCardsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Xml.Linq;

namespace HearthstoneCardsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class CardsController : ControllerBase
    {
        private readonly ILogger<CardsController> _logger;
        private readonly IMongoService _mongoService;
        private readonly Metadata _metadata;
        public CardsController(ILogger<CardsController> logger, IMongoService service)
        {
            _logger = logger;
            _mongoService = service;
            _metadata = service.Database.GetCollection<Metadata>("Metadata").
                Find(x => true).Single();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Card>> GetCards(
            [FromQuery(Name = "page")] int? page,
            [FromQuery(Name = "setid")] int? setid,
            [FromQuery(Name = "artist")] string? artist ,
            [FromQuery(Name = "classid")] int? classid ,
            [FromQuery(Name = "rarityid")] int? rarityid )
        {
            string log = "Cards GET request received. \nParams: ";

            // Filter cards based on query parameters
            var builder = Builders<Card>.Filter;
            var filter = builder.Empty;
            if (artist != null)
            {
                log += "artist: " + artist + " ";
                filter &= builder.Regex(x => x.Artist, new BsonRegularExpression($"/{artist}/i"));
            }

            if (setid != null)
            {
                log += "setid: " + setid.ToString() + " ";
                filter &= builder.Eq(x => x.SetId, setid);
            }

            if (classid != null)
            {
                log += "classid: " + classid.ToString() + " ";
                filter &= builder.Eq(x => x.ClassId, classid);
            }

            if (rarityid != null)
            {
                log += "rarityid: " + rarityid.ToString() + " ";
                filter &= builder.Eq(x => x.RarityId, rarityid);
            }
            _logger.Log(LogLevel.Warning, log);

            int count = 0;
            var collection = _mongoService.Database.GetCollection<Card>("Cards");
            int p = page ?? 1;
            int offset = p * 100 - 100;
            var cards = new List<Card>();
            List<CardGetDto> cardsDto;
            if (page != null)
            {
                cards = collection.Find(filter)
                    .Skip(offset)
                    .Limit(100)
                    .SortBy(x => x.CId)
                    .ToList();
                cardsDto = new List<CardGetDto>(100);
            }
            else
            {
                cards = collection.Find(filter)
                    .Skip(offset)
                    .SortBy(x => x.CId)
                    .ToList();
                cardsDto = new List<CardGetDto>();
            }
            
            
            foreach (var card in cards)
            {
                count++;
                cardsDto.Add(MapToDto(card));
            }

            
            _logger.Log(LogLevel.Warning, "Cards GET request served.");
            _logger.Log(LogLevel.Warning, "Cards returned = " + count);
            return Ok(cardsDto);
        }

        // Map card information to the card return dto. Use metadata to translate appropriate ID numbers
        // To strings. 
        // TODO: Move this to CardGetDtoConstructor instead
        private CardGetDto MapToDto(Card card)
        {
            string className = _metadata.Classes.Find(x => x.CId == card.ClassId).Name;
            string typeName = _metadata.Types.Find(x => x.CId == card.TypeId).Name;
            string rarityName = _metadata.Rarities.Find(x => x.CId == card.RarityId).Name;
            Set? s = _metadata.Sets.Find(x => x.CId == card.SetId);
            string setName = "";
            if (s != null)
            {
                setName = s.Name;
            }
            CardGetDto ret = new CardGetDto()
            {
                CId = card.CId,
                Class = className,
                Name = card.Name,
                ManaCost = card.ManaCost,
                TypeName = typeName,
                Rarity = rarityName,
                Set = setName,
                SpellSchoolId = card.SpellSchoolId,
                Health = card.Health,
                Attack = card.Attack,
                Artist = card.Artist,
                Text = card.Text,
                FlavorText = card.FlavorText
            };
            return ret;
        }
        
    }
}