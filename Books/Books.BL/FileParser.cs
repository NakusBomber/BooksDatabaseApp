using Books.BL.Exceptions;
using Books.BL.Helpers;
using Books.BL.Interfaces;
using Books.BL.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Books.BL;

public class FileParser : IFileParser, IDisposable
{
    private ILineIterator _lineIterator;
    private IUnitWork _unitWork;
    private LineParser? _lineParser;

    public FileParser(ILineIterator lineIterator, IUnitWork unitWork)
    {
        _lineIterator = lineIterator;
        _lineIterator.GoToStart();
        _unitWork = unitWork;
    }
    public FileParser(string pathToFile) : this(
        new LineIterator(pathToFile), 
        new UnitOfWork())
    {
    }

    public async Task ParseBooksAsync()
    {
        var line = await _lineIterator.GetNextLineAsync();

        if(line == null)
        {
            return;
        }

        do
        {
            try
            {
                await LineProcessAsync(line);
            }
            catch (Exception)
            {
                // Ignore this line, if error
            }
            finally
            {
                line = await _lineIterator.GetNextLineAsync();
            }
        } while (line != null);
    }

    public void Dispose()
    {
        _unitWork.Dispose();
    }

    private async Task LineProcessAsync(string line)
    {
        if(string.IsNullOrEmpty(line))
        {
            throw new ArgumentNullException("Line is null or empty");
        }

        _lineParser = new LineParser(line);
        
        var bookTitle = _lineParser.GetTitle();
        var bookPages = _lineParser.GetPages();

        var genre = await AddAndFindIfNeedGenre();
        var author = await AddAndFindIfNeedAuthor();
        var publisher = await AddAndFindIfNeedPublisher();

        var book = new Book(
            bookTitle,
            bookPages,
            genre.Id,
            author.Id,
            publisher.Id,
            _lineParser.GetDate()
        );
        book = await _unitWork.BookRepository.FindEntityAsync(book);
        await _unitWork.BookRepository.InsertAsync(book);

        await _unitWork.SaveAsync();
    }

    private async Task<Genre> AddAndFindIfNeedGenre()
    {
        ArgumentNullException.ThrowIfNull(nameof(_lineParser));

        var genre = new Genre(_lineParser!.GetGenre());
        genre = await _unitWork.GenreRepository.FindEntityAsync(genre);
        await _unitWork.GenreRepository.InsertAsync(genre);
        return genre;
    }
    private async Task<Author> AddAndFindIfNeedAuthor()
    {
        ArgumentNullException.ThrowIfNull(nameof(_lineParser));

        var author = new Author(_lineParser!.GetAuthor());
        author = await _unitWork.AuthorRepository.FindEntityAsync(author);
        await _unitWork.AuthorRepository.InsertAsync(author);
        return author;
    }
    private async Task<Publisher> AddAndFindIfNeedPublisher()
    {
        ArgumentNullException.ThrowIfNull(nameof(_lineParser));

        var publisher = new Publisher(_lineParser!.GetPublisher());
        publisher = await _unitWork.PublisherRepository.FindEntityAsync(publisher);
        await _unitWork.PublisherRepository.InsertAsync(publisher);
        return publisher;
    }
}
