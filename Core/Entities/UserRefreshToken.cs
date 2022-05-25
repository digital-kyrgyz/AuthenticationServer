using System;

namespace Core.Entities
{
    public class UserRefreshToken
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}