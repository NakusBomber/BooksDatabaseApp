using System.ComponentModel.DataAnnotations;

namespace Books.BL.Models;

public class Publisher
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(50)]
    public string? Name { get; set; }
}

