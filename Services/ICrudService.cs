namespace FilmAPI.Services
{
    public interface ICrudService<TEntity, TID>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TID id);
        Task<TEntity> AddAsync(TEntity obj);
        Task<TEntity> UpdateAsync(TEntity obj);
        Task DeleteAsync(TID id);
    }
}
