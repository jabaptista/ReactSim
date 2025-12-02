using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace ReactSim.Repositories
{
    public class MongoDBRepository : IMongoDbRepository
    {

        private readonly IDataContext context;

        public MongoDBRepository(IDataContext context)
        {
            this.context = context;
        }

        public virtual async Task<List<TEntity>> ToListAsync<TEntity>(IFindFluent<TEntity, TEntity> findFluent)
        {
            return await findFluent.ToListAsync().ConfigureAwait(false);
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
        /// FindCursor
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns>A cursor for the query</returns>
        public virtual IFindFluent<TEntity, TEntity> FindCursor<TEntity>(FilterDefinition<TEntity> filter, FindOptions options = null) where TEntity : class, new()
        {
            var collection = this.GetCollection<TEntity>();
            return collection.Find(filter, options);
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

        /// <summary>
        /// A generic count method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<long> CountAsync<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new()
        {
            var collection = this.GetCollection<TEntity>();
            var cursor = collection.Find(filter);
            var count = await cursor.CountDocumentsAsync().ConfigureAwait(false);
            return count;
        }

        #endregion Get

        #region Create

        /// <summary>
        /// A generic Add One method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual async Task<bool> AddOneAsync<TEntity>(TEntity item) where TEntity : class, new()
        {
            var collection = this.GetCollection<TEntity>();
            await collection.InsertOneAsync(item).ConfigureAwait(false);

            return true;
        }

        /// <summary>
        /// A generic Add Many method.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public virtual async Task<bool> AddManyAsync<TEntity>(IEnumerable<TEntity> items) where TEntity : class, new()
        {
            var collection = this.GetCollection<TEntity>();
            await collection.InsertManyAsync(items, new InsertManyOptions { IsOrdered = false }).ConfigureAwait(false);

            return true;
        }

        #endregion Create

        #region Delete

        /// <summary>
        /// A generic delete one method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteOneAsync<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new()
        {
            var collection = this.GetCollection<TEntity>();
            var deleteRes = await collection.DeleteOneAsync(filter).ConfigureAwait(false);
            return true;
        }

        /// <summary>
        /// A generic delete many method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteManyAsync<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new()
        {
            var collection = this.GetCollection<TEntity>();
            var deleteRes = await collection.DeleteManyAsync(filter).ConfigureAwait(false);
            return true;
        }

        #endregion Delete

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

        /// <summary>
        /// UpdateMany with filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateManyAsync<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update) where TEntity : class, new()
        {
            var collection = this.GetCollection<TEntity>();
            var updateRes = await collection.UpdateManyAsync(filter, update).ConfigureAwait(false);

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

        #region Aggregate 

        public virtual IAggregateFluent<TEntity> GetCollectionAggregate<TEntity>() where TEntity : class, new()
        {
            return this.GetCollection<TEntity>().Aggregate();
        }

        public virtual IAggregateFluent<TProjection> GroupStage<TEntity, TKey, TProjection>(IAggregateFluent<TEntity> aggregateFluent, Expression<Func<TEntity, TKey>> id, Expression<Func<IGrouping<TKey, TEntity>, TProjection>> group) where TEntity : class, new()
        {
            return aggregateFluent.Group(id, group);
        }

        public virtual async Task<List<TProjection>> ToListAsync<TProjection>(IAggregateFluent<TProjection> aggregateFluent)
        {
            return await aggregateFluent.ToListAsync().ConfigureAwait(false);
        }

        #endregion

        protected IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return this.context.GetCollection<TEntity>();
        }
    }
}
