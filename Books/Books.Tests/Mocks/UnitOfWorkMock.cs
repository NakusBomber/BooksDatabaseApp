using Books.BL.Interfaces;
using Books.BL.Models;

namespace Books.Tests.Mocks;

public class UnitOfWorkMock : IUnitWork
{
    private IRepository<Book> _bookRepository;
    private IRepository<Author> _authorRepository;
    private IRepository<Genre> _genreRepository;
    private IRepository<Publisher> _publisherRepository;

    public IRepository<Book> BookRepository => _bookRepository;
    public IRepository<Author> AuthorRepository => _authorRepository;
    public IRepository<Genre> GenreRepository => _genreRepository;
    public IRepository<Publisher> PublisherRepository => _publisherRepository;

    public UnitOfWorkMock(
        FakeRepository<Book> bookRepository, 
        FakeRepository<Author> authorRepository, 
        FakeRepository<Genre> genreRepository, 
        FakeRepository<Publisher> publisherRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _genreRepository = genreRepository;
        _publisherRepository = publisherRepository;
    }

    public UnitOfWorkMock() :
        this(new FakeRepository<Book>(), 
            new FakeRepository<Author>(), 
            new FakeRepository<Genre>(), 
            new FakeRepository<Publisher>())
    {
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task SaveAsync()
    {
        await Task.Run(() => { });
    }
}
