using Microsoft.AspNetCore.Identity;

namespace test_back.Models
{
    public class User: IdentityUser
    {
        public string? Name { get; set; }

        public string? Address { get; set; }
        public override string? PhoneNumber { get; set; }
    }
}
