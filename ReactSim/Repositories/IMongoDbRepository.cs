using MongoDB.Driver;
using System.Linq.Expressions;

namespace ReactSim.Repositories
{
    public interface IMongoDbRepository
    {
        /// <summary>
        /// A generic GetOne method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetOneAsync<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new();

        /// <summary>
        /// A generic get many method with filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetManyAsync<TEntity>(FilterDefinition<TEntity> filter, int? limit = null) where TEntity : class, new();

        /// <summary>
        /// GetMany with filter and projection
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="options"></param>
        /// <returns>A cursor for the query</returns>
        IFindFluent<TEntity, TEntity> FindCursor<TEntity>(FilterDefinition<TEntity> filter, FindOptions options = null) where TEntity : class, new();

        /// <summary>
        /// A generic get all method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class, new();

        /// <summary>
        /// A generic count method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<long> CountAsync<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new();

        /// <summary>
        /// A generic Add One method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> AddOneAsync<TEntity>(TEntity item) where TEntity : class, new();

        /// <summary>
        /// A generic Add Many method.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        Task<bool> AddManyAsync<TEntity>(IEnumerable<TEntity> items) where TEntity : class, new();

        /// <summary>
        ///  Delete one with filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<bool> DeleteOneAsync<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new();

        /// <summary>
        /// Delete many with filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<bool> DeleteManyAsync<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new();

        /// <summary>
        /// UpdateOne with filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<bool> UpdateOneAsync<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options = null) where TEntity : class, new();

        /// <summary>
        /// UpdateMany with filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<bool> UpdateManyAsync<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update) where TEntity : class, new();

        /// <summary>
        /// GetAndUpdateOne with filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<TEntity> GetAndUpdateOneAsync<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity, TEntity> options) where TEntity : class, new();

        /// <summary>
        /// Gets the collection aggregate.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        IAggregateFluent<TEntity> GetCollectionAggregate<TEntity>() where TEntity : class, new();

        /// <summary>
        /// The Group Stage of Aggregation.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TProjection">The type of the projection.</typeparam>
        /// <param name="aggregateFluent">The aggregate fluent.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="group">The group.</param>
        /// <returns></returns>
        IAggregateFluent<TProjection> GroupStage<TEntity, TKey, TProjection>(IAggregateFluent<TEntity> aggregateFluent, Expression<Func<TEntity, TKey>> id, Expression<Func<IGrouping<TKey, TEntity>, TProjection>> group) where TEntity : class, new();

        /// <summary>
        /// Lists an Aggregation.
        /// </summary>
        /// <typeparam name="TProjection">The type of the projection.</typeparam>
        /// <param name="aggregateFluent">The aggregate fluent.</param>
        /// <returns></returns>
        Task<List<TProjection>> ToListAsync<TProjection>(IAggregateFluent<TProjection> aggregateFluent);
    }
}
