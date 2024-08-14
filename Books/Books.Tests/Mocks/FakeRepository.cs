using Books.BL.Interfaces;

namespace Books.Tests.Mocks;

public class FakeRepository<T> : IRepository<T> where T : class
{
    private List<T> _entities;

    public FakeRepository(List<T> entities)
    {
        _entities = entities;
    }

    public FakeRepository() : this(new List<T>())
    {
    }

    public async Task<T> FindEntityAsync(T entity)
    {
        var entities = await GetAllEntitiesAsync();
        return entities.Find(e => e.Equals(entity)) ?? entity;
    }

    public async Task<List<T>> GetAllEntitiesAsync()
    {
        return await Task.Run(() => _entities);
    }

    public async Task<bool> InsertAsync(T entity)
    {
        bool Insert()
        {
            if (!_entities.Contains(entity))
            {
                _entities.Add(entity);
                return true;
            }
            return false;
        }
        return await Task.FromResult(Insert());
    }
}
