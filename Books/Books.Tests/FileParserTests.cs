using Books.BL;
using Books.BL.Interfaces;
using Books.Tests.Mocks;

namespace Books.Tests;

public class FileParserTests
{
    private string[] linesFile = "To Kill a Mockingbird,336,Fiction,1960-07-11,Harper Lee,HarperCollins\r\n1984,328,Science Fiction,1949-06-08,George Orwell,Signet Classics".Split("\r\n");
    
    [Theory]
    [InlineData("To Kill a Mockingbird", true)]
    [InlineData("1984", true)]
    [InlineData("Harper", false)]
    public async Task ParseBooksAsync_Tests(string title, bool expected)
    {
        var db = new DatabaseMock();
        var fileParser = new FileParser(new LineIteratorStub(linesFile), db);
        await fileParser.ParseBooksAsync();

        var book = db.Books.ToList().Find((b) => b.Title == title);
        var actual = book! != null!;

        Assert.Equal(expected, actual);
    }

}
