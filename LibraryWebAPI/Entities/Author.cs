using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Entities;

/// <summary>
/// Авторы книг
/// </summary>
public partial class Author
{
    public int AuthorId { get; set; }

    /// <summary>
    /// Имя автора
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Фамилия автора
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Год рождения
    /// </summary>
    public int? BirthYear { get; set; }

    /// <summary>
    /// Год смерти
    /// </summary>
    public int? DeathYear { get; set; }

    /// <summary>
    /// Биография
    /// </summary>
    public string? Biography { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
