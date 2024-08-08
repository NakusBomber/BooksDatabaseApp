using Books.BL;
using Books.BL.Helpers;
using Books.BL.Models;

string path = Console.ReadLine()!.Trim('"');

var fileParser = new FileParser(path);
await fileParser.ParseBooksAsync();

Console.WriteLine("END");
