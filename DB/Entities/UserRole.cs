using Microsoft.AspNetCore.Identity;

namespace SocialMediaAPİ.DB.Entities
{
    public class UserRole : IdentityRole<int>
    {
        public virtual ICollection<IdentityUserRole<int>>? Roles { get; set; }
    }
}
 