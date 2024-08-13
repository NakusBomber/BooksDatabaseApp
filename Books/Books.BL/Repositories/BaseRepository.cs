using Books.BL.Helpers;
using Books.BL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Books.BL.Repositories;

public abstract class BaseRepository<TEntity> : IRepository<TEntity>
{
    protected abstract ApplicationContext Context { get; }
    public abstract Task<List<TEntity>> GetAllEntitiesAsync();
    public abstract Task<TEntity> FindEntityAsync(TEntity entity);
    public abstract Task<bool> InsertAsync(TEntity entity);
    
}
