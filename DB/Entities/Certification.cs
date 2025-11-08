using SocialMediaAPİ.DB.Entities.Base;

namespace SocialMediaAPİ.DB.Entities
{
    public class Certification : AbstractEntity
    {
        public string? Title { get; set; }
        public string? Issuer { get; set; }
        public string? IssuerDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string? CredentialId { get; set; }
        public string? CredentialUrl { get; set; }
    }
}
 