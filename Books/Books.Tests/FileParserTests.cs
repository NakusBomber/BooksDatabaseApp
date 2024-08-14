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
        var unitWork = new UnitOfWorkMock();
        var fileParser = new FileParser(new LineIteratorStub(linesFile), unitWork);
        await fileParser.ParseBooksAsync();

        var books = await unitWork.BookRepository.GetAllEntitiesAsync();
        var book = books.Find((b) => b.Title == title);
        var actual = book! != null!;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task ParseBooksAsync_NoDublicatesData_Tests()
    {
        var unitWork = new UnitOfWorkMock();
        var fileParser = new FileParser(new LineIteratorStub(linesFile), unitWork);
        await fileParser.ParseBooksAsync();
        fileParser = new FileParser(new LineIteratorStub(linesFile), unitWork);
        await fileParser.ParseBooksAsync();

        var books = await unitWork.BookRepository.GetAllEntitiesAsync();
        var expectedCount = 2;
        var actualCount = books.Count();

        Assert.Equal(expectedCount, actualCount);
    }
}
