using Books.BL.Models;

namespace Books.BL.Helpers;

public class LineParser
{
    private string _line;

    public LineParser(string line)
    {
        _line = line;
    }

    public string GetTitle()
    {
        return _line.Split(',')[0];
    }

    public int GetPages()
    {
        return int.Parse(_line.Split(',')[1]);
    }

    public string GetGenre()
    {
        return _line.Split(',')[2];
    }

    public string GetAuthor()
    {
        return _line.Split(',')[4];
    }

    public ExtendedDate GetDate()
    {
        var date = _line.Split(',')[3];
        if(date.Contains("BC") || date.Contains("AD"))
        {
            return ExtendedDate.FromCentury(date.Replace('-', '/'));
        }
        
        return ExtendedDate.Parse(date.Replace('-', '/'));
    }

    public string GetPublisher()
    {
        var chunks = _line.Split(",");
        string[] results = new string[chunks.Length - 5];
        for (int i = 5; i < chunks.Length; i++)
        {
            results[i - 5] = chunks[i];
        }

        return string.Join(',', results);
    }
}
