using Microsoft.AspNetCore.Identity;
using SocialMediaAPİ.Entities.Base;

namespace SocialMediaAPİ.Entities
{
    public class UserRoles : IdentityRole<int>
    {
        public virtual ICollection<IdentityUserRole<int>>? Roles { get; set; }
    }
}
 