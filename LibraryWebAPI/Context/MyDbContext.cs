using System;
using System.Collections.Generic;
using LibraryWebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace LibraryWebAPI.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookCopy> BookCopies { get; set; }

    public virtual DbSet<Borrowing> Borrowings { get; set; }

    public virtual DbSet<BorrowingHistory> BorrowingHistories { get; set; }

    public virtual DbSet<CurrentBorrowingsView> CurrentBorrowingsViews { get; set; }

    public virtual DbSet<Fine> Fines { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<LibraryCard> LibraryCards { get; set; }

    public virtual DbSet<Reader> Readers { get; set; }

    public virtual DbSet<ReaderStatisticsView> ReaderStatisticsViews { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<UserActivityLog> UserActivityLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=192.168.200.13;database=library_db;userid=student;password=student", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.39-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PRIMARY");

            entity.ToTable("authors", tb => tb.HasComment("Авторы книг"));

            entity.HasIndex(e => new { e.LastName, e.FirstName }, "idx_author_name");

            entity.Property(e => e.AuthorId)
                .HasColumnType("int(11)")
                .HasColumnName("author_id");
            entity.Property(e => e.Biography)
                .HasComment("Биография")
                .HasColumnType("text")
                .HasColumnName("biography");
            entity.Property(e => e.BirthYear)
                .HasComment("Год рождения")
                .HasColumnType("int(4)")
                .HasColumnName("birth_year");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DeathYear)
                .HasComment("Год смерти")
                .HasColumnType("int(4)")
                .HasColumnName("death_year");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasComment("Имя автора")
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasComment("Фамилия автора")
                .HasColumnName("last_name");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PRIMARY");

            entity.ToTable("books", tb => tb.HasComment("Книги"));

            entity.HasIndex(e => e.Isbn, "idx_isbn").IsUnique();

            entity.HasIndex(e => e.PublicationYear, "idx_publication_year");

            entity.HasIndex(e => e.Title, "idx_title");

            entity.Property(e => e.BookId)
                .HasColumnType("int(11)")
                .HasColumnName("book_id");
            entity.Property(e => e.AvailableCopies)
                .HasDefaultValueSql("'1'")
                .HasComment("Доступные экземпляры")
                .HasColumnType("int(11)")
                .HasColumnName("available_copies");
            entity.Property(e => e.CoverImageUrl)
                .HasMaxLength(255)
                .HasComment("URL обложки")
                .HasColumnName("cover_image_url");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasComment("Описание/аннотация")
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Isbn)
                .HasMaxLength(25)
                .HasComment("ISBN книги")
                .HasColumnName("isbn");
            entity.Property(e => e.Language)
                .HasMaxLength(20)
                .HasDefaultValueSql("'ru'")
                .HasComment("Язык книги")
                .HasColumnName("language");
            entity.Property(e => e.Pages)
                .HasComment("Количество страниц")
                .HasColumnType("int(11)")
                .HasColumnName("pages");
            entity.Property(e => e.PublicationYear)
                .HasComment("Год издания")
                .HasColumnType("year(4)")
                .HasColumnName("publication_year");
            entity.Property(e => e.Publisher)
                .HasMaxLength(100)
                .HasComment("Издательство")
                .HasColumnName("publisher");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasComment("Название книги")
                .HasColumnName("title");
            entity.Property(e => e.TotalCopies)
                .HasDefaultValueSql("'1'")
                .HasComment("Общее количество экземпляров")
                .HasColumnType("int(11)")
                .HasColumnName("total_copies");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasMany(d => d.Authors).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .HasConstraintName("book_authors_ibfk_2"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .HasConstraintName("book_authors_ibfk_1"),
                    j =>
                    {
                        j.HasKey("BookId", "AuthorId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("book_authors", tb => tb.HasComment("Связь книг с авторами"));
                        j.HasIndex(new[] { "AuthorId" }, "author_id");
                        j.IndexerProperty<int>("BookId")
                            .HasColumnType("int(11)")
                            .HasColumnName("book_id");
                        j.IndexerProperty<int>("AuthorId")
                            .HasColumnType("int(11)")
                            .HasColumnName("author_id");
                    });

            entity.HasMany(d => d.Genres).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .HasConstraintName("book_genres_ibfk_2"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .HasConstraintName("book_genres_ibfk_1"),
                    j =>
                    {
                        j.HasKey("BookId", "GenreId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("book_genres", tb => tb.HasComment("Связь книг с жанрами"));
                        j.HasIndex(new[] { "GenreId" }, "genre_id");
                        j.IndexerProperty<int>("BookId")
                            .HasColumnType("int(11)")
                            .HasColumnName("book_id");
                        j.IndexerProperty<int>("GenreId")
                            .HasColumnType("int(11)")
                            .HasColumnName("genre_id");
                    });
        });

        modelBuilder.Entity<BookCopy>(entity =>
        {
            entity.HasKey(e => e.CopyId).HasName("PRIMARY");

            entity.ToTable("book_copies", tb => tb.HasComment("Экземпляры книг"));

            entity.HasIndex(e => e.IsAvailable, "idx_availability");

            entity.HasIndex(e => new { e.BookId, e.CopyNumber }, "unique_copy").IsUnique();

            entity.Property(e => e.CopyId)
                .HasColumnType("int(11)")
                .HasColumnName("copy_id");
            entity.Property(e => e.AcquiredDate)
                .HasComment("Дата приобретения")
                .HasColumnName("acquired_date");
            entity.Property(e => e.BookId)
                .HasComment("ID книги")
                .HasColumnType("int(11)")
                .HasColumnName("book_id");
            entity.Property(e => e.Condition)
                .HasDefaultValueSql("'good'")
                .HasComment("Состояние")
                .HasColumnType("enum('excellent','good','fair','poor','damaged')")
                .HasColumnName("condition");
            entity.Property(e => e.CopyNumber)
                .HasMaxLength(11)
                .HasComment("Номер экземпляра")
                .HasColumnName("copy_number");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValueSql("'1'")
                .HasComment("Доступен ли экземпляр")
                .HasColumnName("is_available");
            entity.Property(e => e.ShelfLocation)
                .HasMaxLength(50)
                .HasComment("Место на полке")
                .HasColumnName("shelf_location");

            entity.HasOne(d => d.Book).WithMany(p => p.BookCopies)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("book_copies_ibfk_1");
        });

        modelBuilder.Entity<Borrowing>(entity =>
        {
            entity.HasKey(e => e.BorrowingId).HasName("PRIMARY");

            entity.ToTable("borrowings", tb => tb.HasComment("Текущие выдачи книг"));

            entity.HasIndex(e => e.CopyId, "copy_id");

            entity.HasIndex(e => e.DueDate, "idx_due_date");

            entity.HasIndex(e => e.ReaderId, "idx_reader");

            entity.HasIndex(e => e.Status, "idx_status");

            entity.Property(e => e.BorrowingId)
                .HasColumnType("int(11)")
                .HasColumnName("borrowing_id");
            entity.Property(e => e.BorrowDate)
                .HasComment("Дата выдачи")
                .HasColumnName("borrow_date");
            entity.Property(e => e.CopyId)
                .HasComment("ID экземпляра книги")
                .HasColumnType("int(11)")
                .HasColumnName("copy_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DueDate)
                .HasComment("Дата возврата (срок)")
                .HasColumnName("due_date");
            entity.Property(e => e.FineAmount)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'")
                .HasComment("Штраф за просрочку")
                .HasColumnName("fine_amount");
            entity.Property(e => e.Notes)
                .HasComment("Примечания")
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.ReaderId)
                .HasComment("ID читателя")
                .HasColumnType("int(11)")
                .HasColumnName("reader_id");
            entity.Property(e => e.ReturnDate)
                .HasComment("Фактическая дата возврата")
                .HasColumnName("return_date");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'active'")
                .HasComment("Статус выдачи")
                .HasColumnType("enum('active','returned','overdue','lost')")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Copy).WithMany(p => p.Borrowings)
                .HasForeignKey(d => d.CopyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("borrowings_ibfk_2");

            entity.HasOne(d => d.Reader).WithMany(p => p.Borrowings)
                .HasForeignKey(d => d.ReaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("borrowings_ibfk_1");
        });

        modelBuilder.Entity<BorrowingHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PRIMARY");

            entity.ToTable("borrowing_history", tb => tb.HasComment("История всех операций с книгами"));

            entity.HasIndex(e => e.BookId, "idx_book_history");

            entity.HasIndex(e => e.BorrowDate, "idx_borrow_date");

            entity.HasIndex(e => e.ReaderId, "idx_reader_history");

            entity.Property(e => e.HistoryId)
                .HasColumnType("bigint(20)")
                .HasColumnName("history_id");
            entity.Property(e => e.ActualReturnDate)
                .HasComment("Фактическая дата возврата")
                .HasColumnName("actual_return_date");
            entity.Property(e => e.BookId)
                .HasComment("ID книги")
                .HasColumnType("int(11)")
                .HasColumnName("book_id");
            entity.Property(e => e.BorrowDate)
                .HasComment("Дата выдачи")
                .HasColumnName("borrow_date");
            entity.Property(e => e.BorrowingId)
                .HasComment("ID из таблицы borrowings")
                .HasColumnType("int(11)")
                .HasColumnName("borrowing_id");
            entity.Property(e => e.CopyId)
                .HasComment("ID экземпляра")
                .HasColumnType("int(11)")
                .HasColumnName("copy_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DaysOverdue)
                .HasDefaultValueSql("'0'")
                .HasComment("Количество дней просрочки")
                .HasColumnType("int(11)")
                .HasColumnName("days_overdue");
            entity.Property(e => e.DueDate)
                .HasComment("Плановая дата возврата")
                .HasColumnName("due_date");
            entity.Property(e => e.FineAmount)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'")
                .HasComment("Штраф")
                .HasColumnName("fine_amount");
            entity.Property(e => e.OperationType)
                .HasComment("Тип операции")
                .HasColumnType("enum('borrowed','returned','renewed','lost')")
                .HasColumnName("operation_type");
            entity.Property(e => e.ReaderId)
                .HasComment("ID читателя")
                .HasColumnType("int(11)")
                .HasColumnName("reader_id");

            entity.HasOne(d => d.Book).WithMany(p => p.BorrowingHistories)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("borrowing_history_ibfk_2");

            entity.HasOne(d => d.Reader).WithMany(p => p.BorrowingHistories)
                .HasForeignKey(d => d.ReaderId)
                .HasConstraintName("borrowing_history_ibfk_1");
        });

        modelBuilder.Entity<CurrentBorrowingsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("current_borrowings_view");

            entity.Property(e => e.BookTitle)
                .HasMaxLength(200)
                .HasComment("Название книги")
                .HasColumnName("book_title");
            entity.Property(e => e.BorrowDate)
                .HasComment("Дата выдачи")
                .HasColumnName("borrow_date");
            entity.Property(e => e.BorrowingId)
                .HasColumnType("int(11)")
                .HasColumnName("borrowing_id");
            entity.Property(e => e.CopyNumber)
                .HasMaxLength(11)
                .HasComment("Номер экземпляра")
                .HasColumnName("copy_number");
            entity.Property(e => e.DaysOverdue)
                .HasColumnType("int(7)")
                .HasColumnName("days_overdue");
            entity.Property(e => e.DueDate)
                .HasComment("Дата возврата (срок)")
                .HasColumnName("due_date");
            entity.Property(e => e.ReaderId)
                .HasColumnType("int(11)")
                .HasColumnName("reader_id");
            entity.Property(e => e.ReaderName)
                .HasMaxLength(101)
                .HasColumnName("reader_name");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'active'")
                .HasComment("Статус выдачи")
                .HasColumnType("enum('active','returned','overdue','lost')")
                .HasColumnName("status");
        });

        modelBuilder.Entity<Fine>(entity =>
        {
            entity.HasKey(e => e.FineId).HasName("PRIMARY");

            entity.ToTable("fines", tb => tb.HasComment("Штрафы читателей"));

            entity.HasIndex(e => e.BorrowingId, "borrowing_id");

            entity.HasIndex(e => e.ReaderId, "idx_reader_fine");

            entity.HasIndex(e => e.Status, "idx_status");

            entity.Property(e => e.FineId)
                .HasColumnType("int(11)")
                .HasColumnName("fine_id");
            entity.Property(e => e.BorrowingId)
                .HasComment("ID выдачи (если связана)")
                .HasColumnType("int(11)")
                .HasColumnName("borrowing_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.FineAmount)
                .HasPrecision(10, 2)
                .HasComment("Сумма штрафа")
                .HasColumnName("fine_amount");
            entity.Property(e => e.IssueDate)
                .HasComment("Дата выставления")
                .HasColumnName("issue_date");
            entity.Property(e => e.PaidDate)
                .HasComment("Дата оплаты")
                .HasColumnName("paid_date");
            entity.Property(e => e.ReaderId)
                .HasComment("ID читателя")
                .HasColumnType("int(11)")
                .HasColumnName("reader_id");
            entity.Property(e => e.Reason)
                .HasMaxLength(200)
                .HasComment("Причина штрафа")
                .HasColumnName("reason");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'unpaid'")
                .HasComment("Статус оплаты")
                .HasColumnType("enum('unpaid','paid','waived')")
                .HasColumnName("status");

            entity.HasOne(d => d.Borrowing).WithMany(p => p.Fines)
                .HasForeignKey(d => d.BorrowingId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fines_ibfk_2");

            entity.HasOne(d => d.Reader).WithMany(p => p.Fines)
                .HasForeignKey(d => d.ReaderId)
                .HasConstraintName("fines_ibfk_1");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PRIMARY");

            entity.ToTable("genres", tb => tb.HasComment("Жанры книг"));

            entity.HasIndex(e => e.GenreName, "genre_name").IsUnique();

            entity.Property(e => e.GenreId)
                .HasColumnType("int(11)")
                .HasColumnName("genre_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasComment("Описание жанра")
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.GenreName)
                .HasMaxLength(50)
                .HasComment("Название жанра")
                .HasColumnName("genre_name");
        });

        modelBuilder.Entity<LibraryCard>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PRIMARY");

            entity.ToTable("library_cards", tb => tb.HasComment("Читательские билеты"));

            entity.HasIndex(e => e.CardNumber, "card_number").IsUnique();

            entity.HasIndex(e => e.Status, "idx_status");

            entity.HasIndex(e => e.ReaderId, "reader_id");

            entity.Property(e => e.CardId)
                .HasColumnType("int(11)")
                .HasColumnName("card_id");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(20)
                .HasComment("Номер билета")
                .HasColumnName("card_number");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiryDate)
                .HasComment("Дата окончания действия")
                .HasColumnName("expiry_date");
            entity.Property(e => e.IssueDate)
                .HasComment("Дата выдачи билета")
                .HasColumnName("issue_date");
            entity.Property(e => e.Notes)
                .HasComment("Примечания")
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.ReaderId)
                .HasComment("ID читателя")
                .HasColumnType("int(11)")
                .HasColumnName("reader_id");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'active'")
                .HasComment("Статус билета")
                .HasColumnType("enum('active','expired','suspended','lost')")
                .HasColumnName("status");

            entity.HasOne(d => d.Reader).WithMany(p => p.LibraryCards)
                .HasForeignKey(d => d.ReaderId)
                .HasConstraintName("library_cards_ibfk_1");
        });

        modelBuilder.Entity<Reader>(entity =>
        {
            entity.HasKey(e => e.ReaderId).HasName("PRIMARY");

            entity.ToTable("readers", tb => tb.HasComment("Читатели библиотеки"));

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.LastName, "idx_last_name");

            entity.Property(e => e.ReaderId)
                .HasColumnType("int(11)")
                .HasColumnName("reader_id");
            entity.Property(e => e.Address)
                .HasComment("Адрес")
                .HasColumnType("text")
                .HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DateOfBirth)
                .HasComment("Дата рождения")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasComment("Email для личного кабинета")
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasComment("Имя")
                .HasColumnName("first_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasComment("Активен ли читательский билет")
                .HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasComment("Фамилия")
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasComment("Хеш пароля для авторизации")
                .HasColumnName("password_hash");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .HasComment("Отчество")
                .HasColumnName("patronymic");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasComment("Телефон")
                .HasColumnName("phone");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasComment("Дата регистрации")
                .HasColumnType("datetime")
                .HasColumnName("registration_date");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<ReaderStatisticsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("reader_statistics_view");

            entity.Property(e => e.LastReturnDate)
                .HasComment("Фактическая дата возврата")
                .HasColumnName("last_return_date");
            entity.Property(e => e.ReaderId)
                .HasColumnType("int(11)")
                .HasColumnName("reader_id");
            entity.Property(e => e.ReaderName)
                .HasMaxLength(101)
                .HasColumnName("reader_name");
            entity.Property(e => e.TotalBooksRead)
                .HasColumnType("bigint(21)")
                .HasColumnName("total_books_read");
            entity.Property(e => e.TotalBorrowings)
                .HasColumnType("bigint(21)")
                .HasColumnName("total_borrowings");
            entity.Property(e => e.TotalFines)
                .HasPrecision(32, 2)
                .HasColumnName("total_fines");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("PRIMARY");

            entity.ToTable("reservations", tb => tb.HasComment("Резервирование книг"));

            entity.HasIndex(e => e.BookId, "book_id");

            entity.HasIndex(e => e.ReaderId, "idx_reader_reservation");

            entity.HasIndex(e => e.Status, "idx_status");

            entity.Property(e => e.ReservationId)
                .HasColumnType("int(11)")
                .HasColumnName("reservation_id");
            entity.Property(e => e.BookId)
                .HasComment("ID книги")
                .HasColumnType("int(11)")
                .HasColumnName("book_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiryDate)
                .HasComment("Дата истечения резерва")
                .HasColumnName("expiry_date");
            entity.Property(e => e.NotificationSent)
                .HasDefaultValueSql("'0'")
                .HasComment("Отправлено ли уведомление")
                .HasColumnName("notification_sent");
            entity.Property(e => e.ReaderId)
                .HasComment("ID читателя")
                .HasColumnType("int(11)")
                .HasColumnName("reader_id");
            entity.Property(e => e.ReservationDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasComment("Дата резервирования")
                .HasColumnType("datetime")
                .HasColumnName("reservation_date");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'pending'")
                .HasComment("Статус резерва")
                .HasColumnType("enum('pending','fulfilled','cancelled','expired')")
                .HasColumnName("status");

            entity.HasOne(d => d.Book).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("reservations_ibfk_2");

            entity.HasOne(d => d.Reader).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.ReaderId)
                .HasConstraintName("reservations_ibfk_1");
        });

        modelBuilder.Entity<UserActivityLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PRIMARY");

            entity.ToTable("user_activity_log", tb => tb.HasComment("Лог активности пользователей"));

            entity.HasIndex(e => e.CreatedAt, "idx_created_at");

            entity.HasIndex(e => e.ReaderId, "idx_reader_activity");

            entity.Property(e => e.LogId)
                .HasColumnType("bigint(20)")
                .HasColumnName("log_id");
            entity.Property(e => e.ActivityType)
                .HasMaxLength(50)
                .HasComment("Тип активности")
                .HasColumnName("activity_type");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasComment("Описание действия")
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.ReaderId)
                .HasComment("ID читателя")
                .HasColumnType("int(11)")
                .HasColumnName("reader_id");

            entity.HasOne(d => d.Reader).WithMany(p => p.UserActivityLogs)
                .HasForeignKey(d => d.ReaderId)
                .HasConstraintName("user_activity_log_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
