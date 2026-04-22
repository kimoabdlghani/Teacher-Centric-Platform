using System.ComponentModel.DataAnnotations;

namespace Teacher_Centric_Platform.Application.Dtos.Auth
{
    /// <summary>
    /// Registration request. The Role field ("Doctor" or "Nurse") determines
    /// which role-specific fields are required.
    /// </summary>
    public class RegisterRequestDto
    {
        // ── Auth Credentials ───────────────────────────────────────────
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;

        // ── Common AppUser Fields ──────────────────────────────────────
        [Required]
        public string FullName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// 0 = Male, 1 = Female  (maps to Gender enum)
        /// </summary>
        public int Gender { get; set; }

        [Required(ErrorMessage = "الرقم القومي مطلوب")]
        [StringLength(14, MinimumLength = 14,
          ErrorMessage = "الرقم القومي يجب أن يكون 14 رقمًا")]
        [RegularExpression(@"^\d{14}$",
          ErrorMessage = "الرقم القومي يجب أن يحتوي على أرقام فقط")]
        public string NationalId { get; set; } = string.Empty;

    }
}
