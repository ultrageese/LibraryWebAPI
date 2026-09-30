using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Entities;

/// <summary>
/// Экземпляры книг
/// </summary>
public partial class BookCopy
{
    public int CopyId { get; set; }

    /// <summary>
    /// ID книги
    /// </summary>
    public int BookId { get; set; }

    /// <summary>
    /// Номер экземпляра
    /// </summary>
    public string CopyNumber { get; set; } = null!;

    /// <summary>
    /// Место на полке
    /// </summary>
    public string? ShelfLocation { get; set; }

    /// <summary>
    /// Состояние
    /// </summary>
    public string? Condition { get; set; }

    /// <summary>
    /// Доступен ли экземпляр
    /// </summary>
    public bool? IsAvailable { get; set; }

    /// <summary>
    /// Дата приобретения
    /// </summary>
    public DateOnly? AcquiredDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
}
