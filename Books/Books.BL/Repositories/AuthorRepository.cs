using Books.BL.Helpers;
using Books.BL.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.BL.Repositories;

public class AuthorRepository : BaseRepository<Author>
{
    private ApplicationContext _context;
    protected override ApplicationContext Context => _context;

    public AuthorRepository(ApplicationContext context)
    {
        _context = context;
    }

    public override async Task<List<Author>> GetAllEntitiesAsync()
    {
        return await Context.Authors.ToListAsync();
    }

    public override async Task<bool> InsertAsync(Author entity)
    {
        if (!(await Context.Authors.ContainsAsync(entity)))
        {
            await Context.Authors.AddAsync(entity);
            return true;
        }
        return false;
    }

    public override async Task<Author> FindEntityAsync(Author entity)
    {
        var entities = await GetAllEntitiesAsync();
        return entities.Find(e => e == entity) ?? entity;
    }
}
