using MongoDB.Bson.Serialization.Attributes;

namespace HearthstoneCardsAPI.Models;

public class CardType {
    [BsonElement("Id")]
    public int CId { get; set; }
    public String Name { get; set; }
}