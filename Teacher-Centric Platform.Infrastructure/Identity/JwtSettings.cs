namespace Teacher_Centric_Platform.Infrastructure.Identity
{
    /// <summary>
    /// Strongly-typed JWT configuration settings.
    /// Bind from "JwtSettings" section in appsettings.json.
    /// </summary>
    public class JwtSettings
    {
        public const string SectionName = "JwtSettings";

        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = "Teacher_Centric";
        public string Audience { get; set; } = "Teacher_Centric_Users";
        public int DurationInDays { get; set; } = 1;
    }
}
