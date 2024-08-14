﻿using System.ComponentModel.DataAnnotations;

namespace Books.BL.Models;

public class Genre
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(50)]
    public string? Name { get; set; }

    public Genre(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public static bool operator ==(Genre a, Genre b)
    {
        if (a is null)
            return b is null;

        return a.Equals(b);
    }

    public static bool operator !=(Genre a, Genre b)
    {
        return !(a == b);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        return obj is Genre b
                    ? (Name == b.Name)
                    : false;
    }

    public override int GetHashCode()
    {
        return (Id, Name).GetHashCode();
    }
}
