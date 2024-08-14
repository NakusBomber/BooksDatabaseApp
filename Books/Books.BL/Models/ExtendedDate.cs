using System.Diagnostics.CodeAnalysis;

namespace Books.BL.Models;

public struct ExtendedDate
{
    private int _year;
    private int _month;
    private int _day;

    public int Year => _year;
    public int Month => _month;
    public int Day => _day;

    public int MinYear => -9998;
    public int MaxYear => 9999;

    public ExtendedDate(int year, int month, int day)
    {
        if(year < MinYear || year > MaxYear)
        {
            throw new ArgumentOutOfRangeException(nameof(year));
        }

        if(month < 1 || month > 12)
        {
            throw new ArgumentOutOfRangeException(nameof(month));
        }

        if(day < 1 || day > GetMaxDaysInMonth(year, month))
        {
            throw new ArgumentOutOfRangeException(nameof(day));
        }

        _year = year;
        _month = month;
        _day = day;
    }

    public bool IsYearHighEnd()
    {
        return IsYearHighEnd(_year);
    }

    public override string ToString()
    {
        return $"{_year}/{_month}/{_day}";
    }

    public static ExtendedDate Parse(string s)
    {
        if(string.IsNullOrEmpty(s))
        {
            throw new ArgumentNullException("s is null or empty");
        }

        var chunks = s.Trim().Split('/');

        if(chunks.Length != 3)
        {
            throw new ArgumentException("string is not valid");
        }

        return new ExtendedDate(int.Parse(chunks[0]), int.Parse(chunks[1]), int.Parse(chunks[2]));
    }

    public static ExtendedDate FromCentury(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            throw new ArgumentNullException("s is null or empty");
        }

        if (!s.Contains("BC") && !s.Contains("AD"))
        {
            throw new InvalidOperationException("Line not contains any century designation.");
        }

        var chunks = s.Split("th century ");

        if(chunks.Length != 2)
        {
            throw new InvalidOperationException("Invalid string with century.");
        }

        var century = int.Parse(chunks[0]);

        if (century == 0)
        {
            throw new ArgumentException("Century must not be 0.");
        }

        var isBeforeCentury = chunks[1] == "BC";
        var year = isBeforeCentury ? century * -100 : century * 100;

        return new ExtendedDate(year, 12, 31);
    }

    public static bool operator <(ExtendedDate date1, ExtendedDate date2)
    {
        if (date1.Year < date2.Year)
        {
            return true;
        }
        if (date1.Year == date2.Year && date1.Month < date2.Month)
        {
            return true;
        }
        if (date1.Year == date2.Year && date1.Month == date2.Month && date1.Day < date2.Day)
        {
            return true;
        }

        return false;
    }

    public static bool operator >(ExtendedDate date1, ExtendedDate date2)
    {
        return date2 < date1;
    }

    public static bool operator <=(ExtendedDate date1, ExtendedDate date2)
    {
        return date1 < date2 || date1 == date2;
    }

    public static bool operator >=(ExtendedDate date1, ExtendedDate date2)
    {
        return date1 > date2 || date1 == date2;
    }

    public static bool operator ==(ExtendedDate date1, ExtendedDate date2)
    {
        return date1.Equals(date2);
    }

    public static bool operator !=(ExtendedDate date1, ExtendedDate date2)
    {
        return !(date1 == date2);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        ExtendedDate other = (ExtendedDate)obj;
        return Year == other.Year && Month == other.Month && Day == other.Day;
    }
    public override int GetHashCode()
    {
        return (Year, Month, Day).GetHashCode();
    }
    
    private bool IsYearHighEnd(int year)
    {
        return year % 4 == 0;
    }

    private int GetMaxDaysInMonth(int year, int month)
    {
        if (year < MinYear || year > MaxYear)
        {
            throw new ArgumentOutOfRangeException(nameof(year));
        }

        if (month < 1 || month > 12)
        {
            throw new ArgumentOutOfRangeException(nameof(month));
        }

        if (month == 2)
        {
            return IsYearHighEnd(year) ? 29 : 28;
        }
        if (month < 8)
        {
            return month % 2 == 1 ? 31 : 30;
        }

        return month % 2 == 0 ? 31 : 30;
    }
}
