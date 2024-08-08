using Books.BL.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Books.BL.Converters;

public class StringToExtendedDateConverter : ValueConverter<ExtendedDate, string>
{
    public StringToExtendedDateConverter() : base(date => DateToString(date), s => StringToDate(s))
    {

    }

    public static string DateToString(ExtendedDate date) => date.ToString();

    public static ExtendedDate StringToDate(string value)
    {
        return ExtendedDate.Parse(value);
    }
}
