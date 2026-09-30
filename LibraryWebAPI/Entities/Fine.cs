using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Entities;

/// <summary>
/// Штрафы читателей
/// </summary>
public partial class Fine
{
    public int FineId { get; set; }

    /// <summary>
    /// ID читателя
    /// </summary>
    public int ReaderId { get; set; }

    /// <summary>
    /// ID выдачи (если связана)
    /// </summary>
    public int? BorrowingId { get; set; }

    /// <summary>
    /// Сумма штрафа
    /// </summary>
    public decimal FineAmount { get; set; }

    /// <summary>
    /// Причина штрафа
    /// </summary>
    public string Reason { get; set; } = null!;

    /// <summary>
    /// Дата выставления
    /// </summary>
    public DateOnly IssueDate { get; set; }

    /// <summary>
    /// Дата оплаты
    /// </summary>
    public DateOnly? PaidDate { get; set; }

    /// <summary>
    /// Статус оплаты
    /// </summary>
    public string? Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Borrowing? Borrowing { get; set; }

    public virtual Reader Reader { get; set; } = null!;
}
