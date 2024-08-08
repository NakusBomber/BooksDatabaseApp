using Books.BL.Exceptions;
using Books.BL.Helpers;
using Books.BL.Interfaces;
using Books.BL.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Books.BL;

public class FileParser
{
    private ILineIterator _lineIterator;
    private DatabaseHelper _dbHelper;
    public FileParser(ILineIterator lineIterator)
    {
        _lineIterator = lineIterator;
        _lineIterator.GoToStart();
        _dbHelper = new DatabaseHelper();
    }
    public FileParser(string pathToFile) : this(new LineIterator(pathToFile))
    {
    }

    public Filter ParseFilter()
    {
        // TODO : create this method
        return new Filter();
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

        var genre = await _dbHelper.GetAndAddGenreAsync(new Genre(lineParser.GetGenre()));
        var author = await _dbHelper.GetAndAddAuthorAsync(new Author(lineParser.GetAuthor()));
        var publisher = await _dbHelper.GetAndAddPublisherAsync(new Publisher(lineParser.GetPublisher()));
        var book = await _dbHelper.GetAndAddBookAsync(new Book(
            bookTitle,
            bookPages,
            genre.Id,
            author.Id,
            publisher.Id,
            lineParser.GetDateTime()
        ));
    }

}
