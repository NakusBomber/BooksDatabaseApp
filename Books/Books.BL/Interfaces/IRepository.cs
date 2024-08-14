namespace Books.BL.Interfaces;

public interface IRepository<TEntity>
{
    public Task<List<TEntity>> GetAllEntitiesAsync();
    public Task<TEntity> FindEntityAsync(TEntity entity);
    public Task<bool> InsertAsync(TEntity entity);
}
