using MongoDB.Driver;

namespace HearthstoneCardsAPI.Services;

public interface IMongoService
{
    MongoClient Client { get; }
    IMongoDatabase Database { get; }
}