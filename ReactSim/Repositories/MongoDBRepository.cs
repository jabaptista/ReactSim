using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using ReactSim.Repositories.dbo;

namespace ReactSim.Repositories
{
    public class MongoDBRepository : IMongoDbRepository
    {

        private readonly IDataContext context;

        public MongoDBRepository(IDataContext context)
        {
            this.context = context;
        }

        #region Get

        /// <summary>
        /// A generic GetOne method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetOneAsync<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new()
        {
            var collection = this.GetCollection<TEntity>();
            return await collection.Find(filter).SingleOrDefaultAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// A generic get many method with filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetManyAsync<TEntity>(FilterDefinition<TEntity> filter, int? limit = null) where TEntity : class, new()
        {
            var collection = this.GetCollection<TEntity>();
            return await collection.Find(filter).Limit(limit).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// A generic get all method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class, new()
        {
            var collection = this.GetCollection<TEntity>();
            return await collection.Find(new BsonDocument()).ToListAsync().ConfigureAwait(false);
        }

        #endregion Get

        #region Create

        /// <summary>
        /// A generic Add One method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual async Task<bool> AddOneAsync<TEntity>(TEntity item, bool upsert = false) where TEntity : class, new()
        {

            var collection = this.GetCollection<TEntity>();

            if (upsert)
            {
                var filter = BuildIdFilter(item);
                if (filter != null)
                {
                    await collection.ReplaceOneAsync(filter, item, new ReplaceOptions { IsUpsert = true }).ConfigureAwait(false);
                    return true;
                }
            }

            await collection.InsertOneAsync(item).ConfigureAwait(false);

            return true;
        }

        /// <summary>
        /// A generic Add Many method.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public virtual async Task<bool> AddManyAsync<TEntity>(IEnumerable<TEntity> items, bool upsert = false) where TEntity : class, new()
        {
            var collection = this.GetCollection<TEntity>();

            if (upsert)
            {
                foreach (var item in items)
                {
                    await AddOneAsync(item, true).ConfigureAwait(false);
                }
                return true;
            }

            await collection.InsertManyAsync(items, new InsertManyOptions { IsOrdered = false }).ConfigureAwait(false);

            return true;
        }

        #endregion Create

        #region Update

        /// <summary>
        /// UpdateOne with filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateOneAsync<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options = null) where TEntity : class, new()
        {
            var collection = this.GetCollection<TEntity>();
            var updateRes = await collection.UpdateOneAsync(filter, update, options).ConfigureAwait(false);

            return true;
        }

        #endregion Update

        #region Find And Update

        /// <summary>
        /// GetAndUpdateOne with filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetAndUpdateOneAsync<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity, TEntity> options) where TEntity : class, new()
        {
            var result = new TEntity();
            var collection = this.GetCollection<TEntity>();
            result = await collection.FindOneAndUpdateAsync(filter, update, options).ConfigureAwait(false);
            return result;
        }

        #endregion Find And Update

        private static FilterDefinition<TEntity>? BuildIdFilter<TEntity>(TEntity item)
        {
            if (item is MongoEntity mongoEntity && mongoEntity.Id != null)
            {
                return Builders<TEntity>.Filter.Eq("_id", mongoEntity.Id);
            }

            return null;
        }

        protected IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return this.context.GetCollection<TEntity>();
        }
    }
}

