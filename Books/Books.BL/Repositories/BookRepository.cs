using Books.BL.Helpers;
using Books.BL.Interfaces;
using Books.BL.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.BL.Repositories;

public class BookRepository : BaseRepository<Book>
{
    private ApplicationContext _context;
    protected override ApplicationContext Context => _context;

    public BookRepository(ApplicationContext context)
    {
        _context = context;
    }

    public override async Task<List<Book>> GetAllEntitiesAsync()
    {
        return await Context.Books.ToListAsync();
    }

    public override async Task<bool> InsertAsync(Book entity)
    {
        if (!(await Context.Books.ContainsAsync(entity)))
        {
            await Context.Books.AddAsync(entity);
            return true;
        }
        return false;
    }

    public override async Task<Book> FindEntityAsync(Book entity)
    {
        var entities = await GetAllEntitiesAsync();
        return entities.Find(e => e == entity) ?? entity;
    }
}
 