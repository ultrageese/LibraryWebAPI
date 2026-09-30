using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Entities;

public partial class ReaderStatisticsView
{
    public int ReaderId { get; set; }

    public string? ReaderName { get; set; }

    public long TotalBooksRead { get; set; }

    public long TotalBorrowings { get; set; }

    /// <summary>
    /// Фактическая дата возврата
    /// </summary>
    public DateOnly? LastReturnDate { get; set; }

    public decimal? TotalFines { get; set; }
}
