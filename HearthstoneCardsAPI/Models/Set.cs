using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace HearthstoneCardsAPI.Models
{
    public class Set {
        [BsonElement("Id")]
        public int CId { get; set; }
        public String Name { get; set; }
        public String Type { get; set; }
        [BsonElement("collectibleCount")]
        public int collectibleCount { get; set; }
    }
}
