namespace FilmAPI.Services
{
    /// <summary>
    /// Generic interface for CRUD (Create, Read, Update, Delete) operations on entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to perform CRUD operations on.</typeparam>
    /// <typeparam name="TID">The type of the entity's identifier.</typeparam>
    public interface ICrudService<TEntity, TID>
    {
        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>An enumerable collection of all entities.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Gets an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to retrieve.</param>
        /// <returns>The entity with the specified identifier.</returns>
        Task<TEntity> GetByIdAsync(TID id);

        /// <summary>
        /// Adds a new entity.
        /// </summary>
        /// <param name="obj">The entity to add.</param>
        /// <returns>The added entity.</returns>
        Task<TEntity> AddAsync(TEntity obj);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="obj">The entity with updated data.</param>
        /// <returns>The updated entity.</returns>
        Task<TEntity> UpdateAsync(TEntity obj);

        /// <summary>
        /// Deletes an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to delete.</param>
        Task DeleteAsync(TID id);
    }

}
