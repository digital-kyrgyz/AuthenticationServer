using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class UserApp : IdentityUser
    {
        public string City { get; set; }
    }
}