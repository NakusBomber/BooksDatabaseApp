using Books.BL.Exceptions;
using Books.BL.Interfaces;
using Books.BL.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.BL.Helpers;

public class DatabaseHelper : IDatabaseController
{
    private ApplicationContext? _db;

    private List<Book>? _books;
    private List<Genre>? _genres;
    private List<Author>? _authors;
    private List<Publisher>? _publishers;

    public IReadOnlyList<Book> Books => _books ?? new List<Book>();

    public IReadOnlyList<Genre> Genres => _genres ?? new List<Genre>();

    public IReadOnlyList<Author> Authors => _authors ?? new List<Author>();

    public IReadOnlyList<Publisher> Publishers => _publishers ?? new List<Publisher>();

    public async Task SaveChangesAsync()
    {
        ThrowIfDbNull();

        await _db!.SaveChangesAsync();
    }

    public async Task RunTasksAsync()
    {
        _books = await GetAllBooksAsync();
        _genres = await GetAllGenresAsync();
        _authors = await GetAllAuthorsAsync();
        _publishers = await GetAllPublishersAsync();
    }

    public async Task<bool> AddGenreAsync(Genre genre)
    {
        ThrowIfDbNull();

        if (_genres == null)
        {
            throw new ArgumentNullException(nameof(_genres));
        }

        if (!_genres.Contains(genre))
        {
            _genres.Add(genre);
            await _db!.Genres.AddAsync(genre);
            return true;
        }
        return false;
    }
    public Genre GetGenre(Genre genre)
    {
        if (_genres == null)
        {
            throw new ArgumentNullException(nameof(_genres));
        }

        return _genres.Find((g) => g == genre) ?? genre;
    }
    public async Task<bool> AddAuthorAsync(Author author)
    {
        ThrowIfDbNull();

        if (_authors == null)
        {
            throw new ArgumentNullException(nameof(_authors));
        }

        if (!_authors.Contains(author))
        {
            _authors.Add(author);
            await _db!.Authors.AddAsync(author);
            return true;
        }
        return false;
    }
    public Author GetAuthor(Author author)
    {
        if (_authors == null)
        {
            throw new ArgumentNullException(nameof(_authors));
        }

        return _authors.Find((a) => a == author) ?? author;
    }
    public async Task<bool> AddPublisherAsync(Publisher publisher)
    {
        ThrowIfDbNull();

        if (_publishers == null)
        {
            throw new ArgumentNullException(nameof(_publishers));
        }

        if (!_publishers.Contains(publisher))
        {
            _publishers.Add(publisher);
            await _db!.Publishers.AddAsync(publisher);
            return true;
        }
        return false;
    }
    public Publisher GetPublisher(Publisher publisher)
    {
        if (_publishers == null)
        {
            throw new ArgumentNullException(nameof(_publishers));
        }

        return _publishers.Find((p) => p == publisher) ?? publisher;
    }
    public async Task<bool> AddBookAsync(Book book)
    {
        ThrowIfDbNull();

        if (_books == null)
        {
            throw new ArgumentNullException(nameof(_books));
        }

        if (!_books.Contains(book))
        {
            _books.Add(book);
            await _db!.Books.AddAsync(book);
            return true;
        }
        return false;
    }
    public Book GetBook(Book book)
    {
        if (_books == null)
        {
            throw new ArgumentNullException(nameof(_books));
        }

        return _books.Find((b) => b == book) ?? book;
    }
    public async Task CreateConnectionAsync()
    {
        if (_db != null)
        {
            return;
        }

        _db = new ApplicationContext();
        var canConnect = await _db.Database.CanConnectAsync();

        if (!canConnect)
        {
            throw new DbCannotConnectException();
        }

        await _db.Database.OpenConnectionAsync();
    }
    public async Task CloseConnectionAsync()
    {
        if (_db != null)
        {
            await _db.Database.CloseConnectionAsync();
            _db = null;
        }
    }

    private async Task<List<Book>> GetAllBooksAsync()
    {
        ThrowIfDbNull();

        return await _db!.Books.ToListAsync();
    }

    private async Task<List<Author>> GetAllAuthorsAsync()
    {
        ThrowIfDbNull();

        return await _db!.Authors.ToListAsync();
    }

    private async Task<List<Genre>> GetAllGenresAsync()
    {
        ThrowIfDbNull();

        return await _db!.Genres.ToListAsync();
    }

    private async Task<List<Publisher>> GetAllPublishersAsync()
    {
        ThrowIfDbNull();

        return await _db!.Publishers.ToListAsync();
    }

    private void ThrowIfDbNull()
    {
        if (_db == null)
        {
            throw new InvalidOperationException("Database connection is not open");
        }
    }

    ~DatabaseHelper()
    {
        CloseConnectionAsync().GetAwaiter().GetResult();
    }
}
