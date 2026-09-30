using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Entities;

/// <summary>
/// Жанры книг
/// </summary>
public partial class Genre
{
    public int GenreId { get; set; }

    /// <summary>
    /// Название жанра
    /// </summary>
    public string GenreName { get; set; } = null!;

    /// <summary>
    /// Описание жанра
    /// </summary>
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
