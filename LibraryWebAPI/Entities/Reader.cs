using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Entities;

/// <summary>
/// Читатели библиотеки
/// </summary>
public partial class Reader
{
    public int ReaderId { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Отчество
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Email для личного кабинета
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Телефон
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Хеш пароля для авторизации
    /// </summary>
    public string PasswordHash { get; set; } = null!;

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateOnly? DateOfBirth { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Дата регистрации
    /// </summary>
    public DateTime? RegistrationDate { get; set; }

    /// <summary>
    /// Активен ли читательский билет
    /// </summary>
    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<BorrowingHistory> BorrowingHistories { get; set; } = new List<BorrowingHistory>();

    public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();

    public virtual ICollection<Fine> Fines { get; set; } = new List<Fine>();

    public virtual ICollection<LibraryCard> LibraryCards { get; set; } = new List<LibraryCard>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<UserActivityLog> UserActivityLogs { get; set; } = new List<UserActivityLog>();
}
