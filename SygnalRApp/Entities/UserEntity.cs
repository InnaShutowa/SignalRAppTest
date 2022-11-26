using SignalRApp.Extensions;
using System;

namespace SignalRApp.Entities
{
    public class UserEntity : BaseEntity
    {
        public UserEntity() { }
        public UserEntity(string firstName, string lastName, string login, string email, string photo)
        {
            Id = Guid.NewGuid();
            Login = login;
            FirstName = firstName;
            LastName = lastName;
            JpegPhoto = photo;
            EmailPrimary = email;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Hash = this.GenerateStateHash();
        }

        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JpegPhoto { get; set; }
        public string EmailPrimary { get; set; }
        public string Hash { get; set; }
    }
}
