using Books.BL.Helpers;

namespace Books.Tests;

public class LineIteratorTests
{
    [Fact]
    public void NullTest()
    {
        void Test()
        {
            var iter = new LineIterator(null);
        }
        Assert.Throws<ArgumentNullException>(Test);
    }

    [Fact]
    public void EmptyTest()
    {
        void Test()
        {
            var iter = new LineIterator("");
        }
        Assert.Throws<ArgumentException>(Test);
    }

    [Fact]
    public void NotValidDirectory()
    {
        void Test()
        {
            var iter = new LineIterator("/notValid.txt");
        }
        Assert.Throws<FileNotFoundException>(Test);
    }


}
