using Books.BL.Interfaces;

namespace Books.BL.Helpers;

public class LineIterator : ILineIterator
{
    private int _numberLine;
    private StreamReader? _stream = null;
    private string _pathToFile;
    public int NumberLine => _numberLine;

    public LineIterator(string pathToFile)
    {
        _pathToFile = pathToFile;
        GoToStart();
    }

    public void GoToStart()
    {
        _stream = CreateStream(_pathToFile);
        _numberLine = 0;
    }

    public string? GetNextLine()
    {
        return GetNextLineAsync().GetAwaiter().GetResult();
    }

    public async Task<string?> GetNextLineAsync()
    {
        if (_stream == null)
        {
            throw new InvalidOperationException("Stream is not open");
        }

        var line = await _stream.ReadLineAsync();
        if (line == null)
        {
            CloseStream();
            return null;
        }

        _numberLine++;
        return line;
    }

    private StreamReader CreateStream(string pathToFile)
    {
        return File.OpenText(pathToFile);
    }

    private void CloseStream()
    {
        _stream?.Close();
        _stream = null;
    }

    ~LineIterator()
    {
        CloseStream();
    }

}
