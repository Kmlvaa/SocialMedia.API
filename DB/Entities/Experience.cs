using SocialMediaAPİ.Common.Enums;
using SocialMediaAPİ.DB.Entities.Base;

namespace SocialMediaAPİ.DB.Entities
{
    public class Experience : AbstractEntity
    {
        public string? Position { get; set; }
        public string? Company { get; set; }
        public string? CompanyLogoUrl { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public LocationType LocationType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCurrentlyWork { get; set; }
        public List<string> Roles { get; set; } = [];
    }
}
