using Microsoft.AspNetCore.Identity;
using SocialMediaAPİ.Common.Enums;
using SocialMediaAPİ.DB.Entities.Base;

namespace SocialMediaAPİ.DB.Entities
{
    public class User : IdentityUser<int>,IEntity
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? FullName => $"{Name ?? ""} {Surname ?? ""}";

        public string? ProfilePhotoUrl { get; set; }
        public string? Summary { get; set; }
        public string? Position { get; set; }
        public string? Location { get; set; }

        public override string? Email { get; set; }
        public GenderType? Gender { get; set; }

        public List<Experience>? Experiences { get; set; } = [];
        public List<Education>? Educations { get; set; } = [];
        public List<Skill>? Skills { get; set; } = [];
        public List<Certification>? Certifications { get; set; } = [];
        public List<Project>? Projects { get; set; } = [];

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
