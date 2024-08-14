using Books.BL.Helpers;
using Books.BL.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.BL.Repositories;

public class PublisherRepository : BaseRepository<Publisher>
{
    private ApplicationContext _context;
    protected override ApplicationContext Context => _context;

    public PublisherRepository(ApplicationContext context)
    {
        _context = context;
    }

    public override async Task<Publisher> FindEntityAsync(Publisher entity)
    {
        var entities = await GetAllEntitiesAsync();
        return entities.Find(e => e == entity) ?? entity;
    }

    public override async Task<List<Publisher>> GetAllEntitiesAsync()
    {
        return await Context.Publishers.ToListAsync();
    }

    public override async Task<bool> InsertAsync(Publisher entity)
    {
        if (!(await Context.Publishers.ContainsAsync(entity)))
        {
            await Context.Publishers.AddAsync(entity);
            return true;
        }
        return false;
    }
}
