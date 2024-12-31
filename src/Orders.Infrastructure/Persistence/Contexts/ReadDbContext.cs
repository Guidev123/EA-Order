using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Orders.Infrastructure.Persistence.Contexts
{
    public class ReadDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase? _database;
        public ReadDbContext(IConfiguration configuration)
        {
            _configuration = configuration;

            string connectionString = _configuration.GetConnectionString("ReadDbContext") ?? string.Empty;

            MongoUrl mongoUrl = MongoUrl.Create(connectionString);
            MongoClient mongoClient = new(mongoUrl);

            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }
        public IMongoDatabase? Database => _database;
    }
}
