using System.Text.Json;
using HearthstoneCardsAPI.Models;
using MongoDB.Driver;

namespace HearthstoneCardsAPI.Services
{
    public class MongoService : IMongoService{

    private readonly MongoClient _client;

    public MongoService()
    {
        _client = new MongoClient("mongodb://localhost:27017");
        var db = _client.GetDatabase("HearthstoneDb");
        if (db.ListCollections().ToList().Count == 0)
        {
            var collection = db.GetCollection<Card>("cards");
            foreach (var path in new[] { "cards.json", "metadata.json" })
            {
                using (var file = new StreamReader(path))
                {
                    var cards = JsonSerializer.Deserialize<List<Card>>(file.ReadToEnd(), new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    collection.InsertMany(cards);
                }
            }
        }
    }

    public MongoClient Client
    {
        get
        {
            return _client;
        }
    }

    public IMongoDatabase Database
    {
        get
        {
            return _client.GetDatabase("HearthstoneDb");
        }
    }
    }
}
