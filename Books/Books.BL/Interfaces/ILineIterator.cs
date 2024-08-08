namespace Books.BL.Interfaces;

public interface ILineIterator
{
    public int NumberLine { get; }
    public string? GetNextLine();
    public Task<string?> GetNextLineAsync();
    public void GoToStart();

}
