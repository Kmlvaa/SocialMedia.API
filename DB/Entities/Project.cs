using SocialMediaAPİ.DB.Entities.Base;

namespace SocialMediaAPİ.DB.Entities
{
    public class Project : AbstractEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 
        public string? Url { get; set; }
        public List<Skill> TechnologiesUsed { get; set; } = [];
    }
}
 