using ClassLibrary1.Dto;

namespace LibraryWebAPI.Entities
{
    public partial class Reader
    {
        public static explicit operator ReaderDto(Reader r)
        {
            return new ReaderDto
            {
                Id = r.ReaderId,
                FirstName = r.FirstName,
                LastName = r.LastName,
                Patronymic = r.Patronymic,
                Email = r.Email,
                Phone = r.Phone,
                PasswordHash = r.PasswordHash,
                DateOfBirth = r.DateOfBirth,
                Address = r.Address,
                RegistrationDate = r.RegistrationDate,
                IsActive = r.IsActive,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt,
            };
        }
    }
}
