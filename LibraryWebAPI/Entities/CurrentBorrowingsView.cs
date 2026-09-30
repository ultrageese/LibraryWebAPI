using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Entities;

public partial class CurrentBorrowingsView
{
    public int BorrowingId { get; set; }

    public int ReaderId { get; set; }

    public string? ReaderName { get; set; }

    /// <summary>
    /// Название книги
    /// </summary>
    public string BookTitle { get; set; } = null!;

    /// <summary>
    /// Номер экземпляра
    /// </summary>
    public string CopyNumber { get; set; } = null!;

    /// <summary>
    /// Дата выдачи
    /// </summary>
    public DateOnly BorrowDate { get; set; }

    /// <summary>
    /// Дата возврата (срок)
    /// </summary>
    public DateOnly DueDate { get; set; }

    /// <summary>
    /// Статус выдачи
    /// </summary>
    public string? Status { get; set; }

    public int? DaysOverdue { get; set; }
}
