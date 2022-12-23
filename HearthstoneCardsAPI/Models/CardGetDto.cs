using MongoDB.Bson.Serialization.Attributes;

namespace HearthstoneCardsAPI.Models;

// Dto returned from /Cards GET requests
public class CardGetDto
{
    [BsonElement("Id")]
    public int CId { get; set; }
    public String Name { get; set; }
    public String Class { get; set; }
    public String TypeName { get; set; }
    public String Set { get; set; }
    public int? SpellSchoolId { get; set; }
    public String Rarity { get; set; }
    public int? Health { get; set; }
    public int? Attack { get; set; }
    public int ManaCost { get; set; }
    public String Artist { get; set; }
    public String Text { get; set; }
    public String FlavorText { get; set; }   
}