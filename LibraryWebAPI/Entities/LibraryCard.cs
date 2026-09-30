using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Entities;

/// <summary>
/// Читательские билеты
/// </summary>
public partial class LibraryCard
{
    public int CardId { get; set; }

    /// <summary>
    /// ID читателя
    /// </summary>
    public int ReaderId { get; set; }

    /// <summary>
    /// Номер билета
    /// </summary>
    public string CardNumber { get; set; } = null!;

    /// <summary>
    /// Дата выдачи билета
    /// </summary>
    public DateOnly IssueDate { get; set; }

    /// <summary>
    /// Дата окончания действия
    /// </summary>
    public DateOnly ExpiryDate { get; set; }

    /// <summary>
    /// Статус билета
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Примечания
    /// </summary>
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Reader Reader { get; set; } = null!;
}
