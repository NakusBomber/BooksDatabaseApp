using Books.BL.Interfaces;

namespace Books.Tests.Mocks;

public class LineIteratorStub : ILineIterator
{
    private string[] _lines;
    private int _numberLine;
    public int NumberLine => _numberLine;

    public LineIteratorStub(string[] lines)
    {
        _lines = lines;
        GoToStart();
    }
    public string? GetNextLine()
    {
        var index = _numberLine;
        if (index < _lines.Length)
        {
            _numberLine++;
            return _lines[index];
        }
        return null;
    }

    public async Task<string?> GetNextLineAsync()
    {
        return await Task.FromResult(GetNextLine());
    }

    public void GoToStart()
    {
        _numberLine = 0;
    }

}
