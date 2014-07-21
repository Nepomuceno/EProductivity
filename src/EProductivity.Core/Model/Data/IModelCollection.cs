using System.Linq;

namespace EProductivity.Core.Model.Data
{
    public interface IModelCollection<T, in TKey> : IQueryable<T>
    {
        /// <summary>
        /// Gets the entity by its id or returns null
        /// </summary>
        /// <param name="key">The entity primary key</param>
        /// <returns></returns>

        T this[TKey key] { get; }
        /// <summary>
        /// Gets the entity by its Id or throws NotFoundException 
        /// if its not found
        /// </summary>
        T WithId(TKey key);

        /// <summary>
        /// Insert a new entity in the colection
        /// </summary>
        /// <param name="entity">the new entity do be insered</param>
        /// <returns>The collection with the new item included</returns>
        IModelCollection<T, TKey> Add(T entity);

        /// <summary>
        /// Remove a entity in the colection
        /// </summary>
        /// <param name="entity">the entity do be removed</param>
        /// <returns>The collection with the entity removed</returns>
        IModelCollection<T, TKey> Remove(T entity);

    }
}
