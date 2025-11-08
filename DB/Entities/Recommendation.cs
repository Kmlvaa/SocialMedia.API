using SocialMediaAPİ.DB.Entities.Base;

namespace SocialMediaAPİ.DB.Entities
{
    public class Recommendation : AbstractEntity
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string? Content { get; set; }
    }
}
