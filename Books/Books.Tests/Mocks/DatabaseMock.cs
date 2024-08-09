using Books.BL.Interfaces;
using Books.BL.Models;

namespace Books.Tests.Mocks;

public class DatabaseMock : IDatabaseController
{
    private List<Book> _booksTest;
    private List<Genre> _genresTest;
    private List<Publisher> _publishersTest;
    private List<Author> _authorsTest;

    private List<Book> _books = new();
    private List<Genre> _genres = new();
    private List<Publisher> _publishers = new();
    private List<Author> _authors = new();

    public IReadOnlyList<Book> Books => _books;
    public IReadOnlyList<Author> Authors => _authors;

    public IReadOnlyList<Genre> Genres => _genres;

    public IReadOnlyList<Publisher> Publishers => _publishers;

    public DatabaseMock(List<Book> books, List<Author> authors, List<Genre> genres, List<Publisher> publishers)
    {
        _booksTest = books;
        _authorsTest = authors;
        _genresTest = genres;
        _publishersTest = publishers;
    }

    public DatabaseMock(List<Book> books, List<Author> authors)
        : this(books, authors, new List<Genre>(), new List<Publisher>())
    {
    }

    public DatabaseMock()
        : this(new List<Book>(), new List<Author>())
    {
    }
    public async Task<bool> AddAuthorAsync(Author author)
    {
        bool TestFunc()
        {
            if (!_authors.Contains(author))
            {
                _authors.Add(author);
                return true;
            }
            return false;
        }
        return await Task.FromResult(TestFunc());
    }

    public async Task<bool> AddBookAsync(Book book)
    {
        bool TestFunc()
        {
            if (!_books.Contains(book))
            {
                _books.Add(book);
                return true;
            }
            return false;
        }
        return await Task.FromResult(TestFunc());
    }

    public async Task<bool> AddGenreAsync(Genre genre)
    {
        bool TestFunc()
        {
            if (!_genres.Contains(genre))
            {
                _genres.Add(genre);
                return true;
            }
            return false;
        }
        return await Task.FromResult(TestFunc());
    }

    public async Task<bool> AddPublisherAsync(Publisher publisher)
    {
        bool TestFunc()
        {
            if (!_publishers.Contains(publisher))
            {
                _publishers.Add(publisher);
                return true;
            }
            return false;
        }
        return await Task.FromResult(TestFunc());
    }

    public async Task CloseConnectionAsync()
    {
        void Test()
        {
        }
        await Task.FromResult(Test);
    }

    public async Task CreateConnectionAsync()
    {
        void Test()
        {
        }
        await Task.FromResult(Test);
    }

    public Author GetAuthor(Author author)
    {
        return _authors.Find((obj) => obj == author) ?? author;
    }

    public Book GetBook(Book book)
    {
        return _books.Find((obj) => obj == book) ?? book;
    }

    public Genre GetGenre(Genre genre)
    {
        return _genres.Find((obj) => obj == genre) ?? genre;
    }

    public Publisher GetPublisher(Publisher publisher)
    {
        return _publishers.Find((obj) => obj == publisher) ?? publisher;
    }

    public async Task RunTasksAsync()
    {
        void Test()
        {
            _books = _booksTest;
            _authors = _authorsTest;
            _genres = _genresTest;
            _publishers = _publishersTest;
        }
        await Task.FromResult(Test);
    }

    public async Task SaveChangesAsync()
    {
        void Test()
        {
        }
        await Task.FromResult(Test);
    }
}
