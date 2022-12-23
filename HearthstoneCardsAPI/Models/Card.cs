using System.ComponentModel;
using MongoDB.Bson.Serialization.Attributes;

namespace HearthstoneCardsAPI.Models;

public class Card {
    [BsonId]
    [BsonElement("_id")]
    public int ObjectId { get; }
    [BsonElement("Id")]
    public int CId { get; set; }
    public String Name { get; set; }
    public int ClassId { get; set; }
    [BsonElement("cardTypeId")]
    public int TypeId { get; set; }
    [BsonElement("cardSetId")]
    public int SetId { get; set; }
    public int? SpellSchoolId { get; set; }
    public int RarityId { get; set; }
    public int? Health { get; set; }
    public int? Attack { get; set; }
    public int ManaCost { get; set; }
    [BsonElement("artistName")]
    public String Artist { get; set; }
    public String Text { get; set; }
    public String FlavorText { get; set; }
}