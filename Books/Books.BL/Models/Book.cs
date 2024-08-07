using System.ComponentModel.DataAnnotations;

namespace Books.BL.Models;

public class Book
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(200)]
    public string? Title { get; set; }

    [Required]
    public int Pages { get; set; }

    [Required]
    public Guid GenreId { get; set; }

    [Required]
    public Guid AuthorId { get; set; }

    [Required]
    public Guid PublisherId { get; set; }
    public DateTime ReleaseDate { get; set; }

    public Book(string title, int pages, Guid genreId, Guid authorId, Guid publisherId, DateTime releaseDate)
    {
        Id = Guid.NewGuid();
        Title = title;
        Pages = pages;
        GenreId = genreId;
        AuthorId = authorId;
        PublisherId = publisherId;
        ReleaseDate = releaseDate;
    }
}
