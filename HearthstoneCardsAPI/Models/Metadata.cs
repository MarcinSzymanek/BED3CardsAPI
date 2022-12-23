using MongoDB.Bson.Serialization.Attributes;

namespace HearthstoneCardsAPI.Models;

public class Metadata
{
    [BsonId]
    public string Id { get;}
    public int Identifier { get; set; }
    [BsonElement("Sets")]
    public List<Set> Sets { get; set; }
    [BsonElement("Classes")]
    public List<PlayerClass> Classes { get; set; }
    [BsonElement("Rarities")]
    public List<Rarity> Rarities { get; set; }
    [BsonElement("Types")]
    public List<CardType> Types { get; set; } 

}