using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Entities;

/// <summary>
/// Резервирование книг
/// </summary>
public partial class Reservation
{
    public int ReservationId { get; set; }

    /// <summary>
    /// ID читателя
    /// </summary>
    public int ReaderId { get; set; }

    /// <summary>
    /// ID книги
    /// </summary>
    public int BookId { get; set; }

    /// <summary>
    /// Дата резервирования
    /// </summary>
    public DateTime? ReservationDate { get; set; }

    /// <summary>
    /// Статус резерва
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Дата истечения резерва
    /// </summary>
    public DateOnly? ExpiryDate { get; set; }

    /// <summary>
    /// Отправлено ли уведомление
    /// </summary>
    public bool? NotificationSent { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Reader Reader { get; set; } = null!;
}
