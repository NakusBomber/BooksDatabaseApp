using Books.BL.Models;

namespace Books.BL.Interfaces;

public interface IDatabaseController
{
    public IReadOnlyList<Book> Books { get; }
    public IReadOnlyList<Author> Authors { get; }
    public IReadOnlyList<Genre> Genres { get; }
    public IReadOnlyList<Publisher> Publishers { get; }

    public Task SaveChangesAsync();
    public Task RunTasksAsync();

    public Task<bool> AddGenreAsync(Genre genre);
    public Task<bool> AddBookAsync(Book book);
    public Task<bool> AddAuthorAsync(Author author);
    public Task<bool> AddPublisherAsync(Publisher publisher);

    public Genre GetGenre(Genre genre);
    public Book GetBook(Book book);
    public Author GetAuthor(Author author);
    public Publisher GetPublisher(Publisher publisher);

    public Task CreateConnectionAsync();
    public Task CloseConnectionAsync();
}
