
using Books.BL;

internal class Program
{
    private static Task? _task = null;
    private static void Main(string[] args)
    {
        while (true)
        {
            try
            {
                Console.Clear();
                if (_task == null)
                {
                    string path = GetPath();
                    _task = Task.Run(() => Process(path));
                }
                else
                {
                    if(_task.IsFaulted)
                    {
                        throw _task.Exception;
                    }
                    if(_task.IsCompleted)
                    {
                        AllGood();
                    }
                }
                Console.WriteLine("Processing...");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                continue;
            }
            catch (Exception e)
            {
                AllBad();
            }
        }

    }

    private static async Task Process(string path)
    {
        var fileParser = new FileParser(path);
        await fileParser.ParseBooksAsync();
    }
    private static string GetPath()
    {
        string? path = null;

        if (path == null)
        {
            Console.Clear();
            Console.Write("Input absolute path to file: ");
            path = Console.ReadLine();
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path must not be empty!");
            }
            path = path.Trim('"', '\'');
        }

        return path;
    }

    private static void AllGood()
    {
        Console.Clear();
        Console.WriteLine("File success parsed!");
        Console.ReadKey();
        _task = null;
    }

    private static void AllBad()
    {
        Console.Clear();
        Console.WriteLine("File was not parsed!");
        Console.ReadKey();
        _task = null;
    }
}
