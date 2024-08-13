using Books.BL.Models;

namespace Books.BL.Interfaces;

public interface IUnitWork : IDisposable
{
    public IRepository<Book> BookRepository { get; }
    public IRepository<Author> AuthorRepository { get; }
    public IRepository<Genre> GenreRepository { get; }
    public IRepository<Publisher> PublisherRepository { get; }
    public Task SaveAsync();
}
