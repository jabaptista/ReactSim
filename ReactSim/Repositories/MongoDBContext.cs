using MongoDB.Driver;

namespace ReactSim.Repositories
{
    public class MongoDBContext : IDataContext
    {
        private readonly MongoClient client;

        private readonly IMongoDatabase database;

        public MongoDBContext()
        {
            var mongoUrl = MongoUrl.Create(Environment.GetEnvironmentVariable("MONGO_CONNECTION"));

            this.client = new MongoClient(mongoUrl);
            this.database = this.client.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return this.database.GetCollection<TEntity>(typeof(TEntity).Name);
        }
    }
}
