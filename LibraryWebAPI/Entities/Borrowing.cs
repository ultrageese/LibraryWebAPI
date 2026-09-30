using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Entities;

/// <summary>
/// Текущие выдачи книг
/// </summary>
public partial class Borrowing
{
    public int BorrowingId { get; set; }

    /// <summary>
    /// ID читателя
    /// </summary>
    public int ReaderId { get; set; }

    /// <summary>
    /// ID экземпляра книги
    /// </summary>
    public int CopyId { get; set; }

    /// <summary>
    /// Дата выдачи
    /// </summary>
    public DateOnly BorrowDate { get; set; }

    /// <summary>
    /// Дата возврата (срок)
    /// </summary>
    public DateOnly DueDate { get; set; }

    /// <summary>
    /// Фактическая дата возврата
    /// </summary>
    public DateOnly? ReturnDate { get; set; }

    /// <summary>
    /// Статус выдачи
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Штраф за просрочку
    /// </summary>
    public decimal? FineAmount { get; set; }

    /// <summary>
    /// Примечания
    /// </summary>
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual BookCopy Copy { get; set; } = null!;

    public virtual ICollection<Fine> Fines { get; set; } = new List<Fine>();

    public virtual Reader Reader { get; set; } = null!;
}
