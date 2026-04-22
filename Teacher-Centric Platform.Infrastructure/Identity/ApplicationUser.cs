using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Teacher_Centric_Platform.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Full display name of the user.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// 0 = Male, 1 = Female  (maps to Gender enum)
        /// </summary>
        public int Gender { get; set; }
        public string NationalId { get; set; } = string.Empty;
    }
}
