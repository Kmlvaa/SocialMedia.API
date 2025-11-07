using Microsoft.AspNetCore.Identity;
using SocialMediaAPİ.Common.Enums;
using SocialMediaAPİ.Entities.Base;

namespace SocialMediaAPİ.Entities
{
    public class User : IdentityUser<int>,IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? FullName => $"{Name ?? ""} {Surname ?? ""}";
        public string? Position { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public GenderType? Gender { get; set; }
        public UserRoles Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
