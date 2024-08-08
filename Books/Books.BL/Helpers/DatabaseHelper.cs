using Books.BL.Exceptions;
using Books.BL.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.BL.Helpers;

public class DatabaseHelper
{
    private ApplicationContext? _db;

    private List<Book>? _books;
    private List<Genre>? _genres;
    private List<Author>? _authors;
    private List<Publisher>? _publishers;

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

    public async Task<Genre> GetAndAddGenreAsync(Genre genre)
    {
        ThrowIfDbNull();

        if (_genres == null)
        {
            throw new ArgumentNullException(nameof(_genres));
        }

        var newGenre = (from g in _genres
                        where genre == g
                        select g).ToList()
                                 .FirstOrDefault(genre);

        if (newGenre.Id == genre.Id)
        {
            _genres.Add(genre);
            await _db!.Genres.AddAsync(genre);
        }
        return newGenre;
    }

    public async Task<Author> GetAndAddAuthorAsync(Author author)
    {
        ThrowIfDbNull();

        if (_authors == null)
        {
            throw new ArgumentNullException(nameof(_authors));
        }

        var newAuthor = (from a in _authors
                         where author == a
                         select a).ToList()
                                 .FirstOrDefault(author);

        if (newAuthor.Id == author.Id)
        {
            _authors.Add(author);
            await _db!.Authors.AddAsync(author);
        }
        return newAuthor;
    }

    public async Task<Publisher> GetAndAddPublisherAsync(Publisher publisher)
    {
        ThrowIfDbNull();

        if (_publishers == null)
        {
            throw new ArgumentNullException(nameof(_publishers));
        }

        var newPublisher = (from p in _publishers
                            where publisher == p
                            select p).ToList()
                                     .FirstOrDefault(publisher);

        if (newPublisher.Id == publisher.Id)
        {
            _publishers.Add(publisher);
            await _db!.Publishers.AddAsync(publisher);
        }
        return newPublisher;
    }

    public async Task<Book> GetAndAddBookAsync(Book book)
    {
        ThrowIfDbNull();

        if (_books == null)
        {
            throw new ArgumentNullException(nameof(_books));
        }

        var newBook = (from b in _books
                       where book == b
                       select b).ToList()
                                 .FirstOrDefault(book);

        if (newBook.Id == book.Id)
        {
            _books.Add(book);
            await _db!.Books.AddAsync(book);
        }
        return newBook;
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
