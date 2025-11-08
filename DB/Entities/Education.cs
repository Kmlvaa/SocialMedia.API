using SocialMediaAPİ.DB.Entities.Base;

namespace SocialMediaAPİ.DB.Entities
{
    public class Education : AbstractEntity
    {
        public string? Degree { get; set; }
        public string? School { get; set; }
        public string? FieldOfStudy { get; set; }
        public string? SchoolLogoUrl { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public bool IsCurrentlyStudying { get; set; }
        public string? Description { get; set; }
    }
}
