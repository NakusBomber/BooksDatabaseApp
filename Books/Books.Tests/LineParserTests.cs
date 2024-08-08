using Books.BL.Helpers;
using Books.BL.Models;

namespace Books.Tests;

public class LineParserTests
{
    [Fact]
    public void GetTitle_Test()
    {
        var lineParser = new LineParser("The Great Gatsby,180,Classics,1925-04-10,F. Scott Fitzgerald,Scribner");

        var expected = "The Great Gatsby";
        var actual = lineParser.GetTitle();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetPages_Test()
    {
        var lineParser = new LineParser("The Great Gatsby,180,Classics,1925-04-10,F. Scott Fitzgerald,Scribner");

        var expected = 180;
        var actual = lineParser.GetPages();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetGenre_Test()
    {
        var lineParser = new LineParser("The Great Gatsby,180,Classics,1925-04-10,F. Scott Fitzgerald,Scribner");

        var expected = "Classics";
        var actual = lineParser.GetGenre();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetDate_Test()
    {
        var lineParser = new LineParser("The Great Gatsby,180,Classics,1925-04-10,F. Scott Fitzgerald,Scribner");

        var expected = new ExtendedDate(1925, 04, 10);
        var actual = lineParser.GetDate();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetAuthor_Test()
    {
        var lineParser = new LineParser("The Great Gatsby,180,Classics,1925-04-10,F. Scott Fitzgerald,Scribner");

        var expected = "F. Scott Fitzgerald";
        var actual = lineParser.GetAuthor();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetPublisher_Test()
    {
        var lineParser = new LineParser("The Great Gatsby,180,Classics,1925-04-10,F. Scott Fitzgerald,Scribner");

        var expected = "Scribner";
        var actual = lineParser.GetPublisher();

        Assert.Equal(expected, actual);
    }
}