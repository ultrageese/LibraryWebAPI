using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Entities;

/// <summary>
/// История всех операций с книгами
/// </summary>
public partial class BorrowingHistory
{
    public long HistoryId { get; set; }

    /// <summary>
    /// ID из таблицы borrowings
    /// </summary>
    public int BorrowingId { get; set; }

    /// <summary>
    /// ID читателя
    /// </summary>
    public int ReaderId { get; set; }

    /// <summary>
    /// ID книги
    /// </summary>
    public int BookId { get; set; }

    /// <summary>
    /// ID экземпляра
    /// </summary>
    public int CopyId { get; set; }

    /// <summary>
    /// Дата выдачи
    /// </summary>
    public DateOnly BorrowDate { get; set; }

    /// <summary>
    /// Плановая дата возврата
    /// </summary>
    public DateOnly DueDate { get; set; }

    /// <summary>
    /// Фактическая дата возврата
    /// </summary>
    public DateOnly? ActualReturnDate { get; set; }

    /// <summary>
    /// Количество дней просрочки
    /// </summary>
    public int? DaysOverdue { get; set; }

    /// <summary>
    /// Штраф
    /// </summary>
    public decimal? FineAmount { get; set; }

    /// <summary>
    /// Тип операции
    /// </summary>
    public string OperationType { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Reader Reader { get; set; } = null!;
}
