using Books.BL.Models;

namespace Books.Tests;

public class ExtendedDateTests
{
    [Fact]
    public void IsYearHighEnd_Test()
    {
        var date = new ExtendedDate(2004, 11, 1);

        var excepted = true;
        var actual = date.IsYearHighEnd();

        Assert.Equal(excepted, actual);
    }

    [Fact]
    public void IsYearHighEnd_Test2()
    {
        var date = new ExtendedDate(2008, 11, 1);

        var excepted = true;
        var actual = date.IsYearHighEnd();

        Assert.Equal(excepted, actual);
    }

    [Fact]
    public void Parse_Test()
    {
        var excepted = new ExtendedDate(1923, 11, 2);
        var actual = ExtendedDate.Parse("1923/11/2");

        Assert.Equal(excepted, actual);
    }

    [Fact]
    public void FromCentury_Test()
    {
        var excepted = new ExtendedDate(-200, 12, 31);
        var actual = ExtendedDate.FromCentury("2th century BC");

        Assert.Equal(excepted, actual);
    }

    [Theory]
    [InlineData(2012, 3, 32)]
    [InlineData(10000, 3, 2)]
    [InlineData(-9999, 3, 2)]
    [InlineData(1233, 2, 29)]
    [InlineData(2004, 13, 29)]
    [InlineData(2004, -1, 29)]
    [InlineData(2004, 1, -9)]
    public void ExceptionsTest(int year, int month, int day)
    {
        void Test()
        {
            new ExtendedDate(year, month, day);
        }
        Assert.Throws<ArgumentOutOfRangeException>(Test);
    }
}
