using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Entities;

/// <summary>
/// Лог активности пользователей
/// </summary>
public partial class UserActivityLog
{
    public long LogId { get; set; }

    /// <summary>
    /// ID читателя
    /// </summary>
    public int ReaderId { get; set; }

    /// <summary>
    /// Тип активности
    /// </summary>
    public string ActivityType { get; set; } = null!;

    /// <summary>
    /// Описание действия
    /// </summary>
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Reader Reader { get; set; } = null!;
}
