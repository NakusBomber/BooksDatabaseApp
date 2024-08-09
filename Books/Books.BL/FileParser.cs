using Books.BL.Exceptions;
using Books.BL.Helpers;
using Books.BL.Interfaces;
using Books.BL.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Books.BL;

public class FileParser : IFileParser
{
    private ILineIterator _lineIterator;
    private IDatabaseController _dbHelper;
    public FileParser(ILineIterator lineIterator, IDatabaseController dbHelper)
    {
        _lineIterator = lineIterator;
        _lineIterator.GoToStart();
        _dbHelper = dbHelper;
    }
    public FileParser(string pathToFile) : this(new LineIterator(pathToFile), new DatabaseHelper())
    {
    }

    public async Task ParseBooksAsync()
    {
        await _dbHelper.CreateConnectionAsync();

        var line = await _lineIterator.GetNextLineAsync();

        if(line == null)
        {
            await _dbHelper.CloseConnectionAsync();
            return;
        }

        await _dbHelper.RunTasksAsync();

        do
        {
            try
            {
                await LineProcessAsync(line);
            }
            catch (Exception)
            {
            }
            finally
            {
                line = await _lineIterator.GetNextLineAsync();
            }
        } while (line != null);

        await _dbHelper.SaveChangesAsync();
        await _dbHelper.CloseConnectionAsync();
    }

    private async Task LineProcessAsync(string line)
    {
        if(string.IsNullOrEmpty(line))
        {
            throw new ArgumentNullException("Line is null or empty");
        }

        var lineParser = new LineParser(line);

        var bookTitle = lineParser.GetTitle();
        var bookPages = lineParser.GetPages();

        var genre = _dbHelper.GetGenre(new Genre(lineParser.GetGenre()));
        await _dbHelper.AddGenreAsync(genre);
        var author = _dbHelper.GetAuthor(new Author(lineParser.GetAuthor()));
        await _dbHelper.AddAuthorAsync(author);
        var publisher = _dbHelper.GetPublisher(new Publisher(lineParser.GetPublisher()));
        await _dbHelper.AddPublisherAsync(publisher);
        var book = _dbHelper.GetBook(new Book(
            bookTitle,
            bookPages,
            genre.Id,
            author.Id,
            publisher.Id,
            lineParser.GetDate()
        ));
        await _dbHelper.AddBookAsync(book);
    }

}
