namespace Teacher_Centric_Platform.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string userId, string userName, string email, IEnumerable<string> roles);
    }
}
