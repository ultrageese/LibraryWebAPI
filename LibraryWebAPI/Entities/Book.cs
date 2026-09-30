using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Entities;

/// <summary>
/// Книги
/// </summary>
public partial class Book
{
    public int BookId { get; set; }

    /// <summary>
    /// ISBN книги
    /// </summary>
    public string? Isbn { get; set; }

    /// <summary>
    /// Название книги
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Год издания
    /// </summary>
    public short? PublicationYear { get; set; }

    /// <summary>
    /// Издательство
    /// </summary>
    public string? Publisher { get; set; }

    /// <summary>
    /// Количество страниц
    /// </summary>
    public int? Pages { get; set; }

    /// <summary>
    /// Язык книги
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// Описание/аннотация
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// URL обложки
    /// </summary>
    public string? CoverImageUrl { get; set; }

    /// <summary>
    /// Общее количество экземпляров
    /// </summary>
    public int? TotalCopies { get; set; }

    /// <summary>
    /// Доступные экземпляры
    /// </summary>
    public int? AvailableCopies { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<BookCopy> BookCopies { get; set; } = new List<BookCopy>();

    public virtual ICollection<BorrowingHistory> BorrowingHistories { get; set; } = new List<BorrowingHistory>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
