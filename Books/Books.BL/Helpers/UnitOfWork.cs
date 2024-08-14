using Books.BL.Interfaces;
using Books.BL.Models;
using Books.BL.Repositories;

namespace Books.BL.Helpers;

public class UnitOfWork : IUnitWork
{
    private ApplicationContext _context = new ApplicationContext();
    private IRepository<Book>? _bookRepository;
    private IRepository<Author>? _authorRepository;
    private IRepository<Genre>? _genreRepository;
    private IRepository<Publisher>? _publisherRepository;
    public IRepository<Book> BookRepository
    {
        get
        {
            if (_bookRepository == null)
            {
                _bookRepository = new BookRepository(_context);
            }
            return _bookRepository;
        }
    }

    public IRepository<Author> AuthorRepository
    {
        get
        {
            if (_authorRepository == null)
            {
                _authorRepository = new AuthorRepository(_context);
            }
            return _authorRepository;
        }
    }

    public IRepository<Genre> GenreRepository
    {
        get
        {
            if (_genreRepository == null)
            {
                _genreRepository = new GenreRepository(_context);
            }
            return _genreRepository;
        }
    }

    public IRepository<Publisher> PublisherRepository
    {
        get
        {
            if (_publisherRepository == null)
            {
                _publisherRepository = new PublisherRepository(_context);
            }
            return _publisherRepository;
        }
    }


    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
