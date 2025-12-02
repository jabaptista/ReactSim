using MongoDB.Driver;

namespace ReactSim.Repositories
{
    public interface IDataContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>();
    }
}
