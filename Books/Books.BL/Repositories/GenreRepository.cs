using Books.BL.Helpers;
using Books.BL.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.BL.Repositories;

public class GenreRepository : BaseRepository<Genre>
{
    private ApplicationContext _context;
    protected override ApplicationContext Context => _context;

    public GenreRepository(ApplicationContext context)
    {
        _context = context;
    }

    public override async Task<Genre> FindEntityAsync(Genre entity)
    {
        var entities = await GetAllEntitiesAsync();
        return entities.Find(e => e == entity) ?? entity;
    }

    public override async Task<List<Genre>> GetAllEntitiesAsync()
    {
        return await Context.Genres.ToListAsync();
    }

    public override async Task<bool> InsertAsync(Genre entity)
    {
        if (!(await Context.Genres.ContainsAsync(entity)))
        {
            await Context.Genres.AddAsync(entity);
            return true;
        }
        return false;
    }
}
