using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Teacher_Centric_Platform.Application.Common.Interfaces;
using Teacher_Centric_Platform.Application.Dtos;
using Teacher_Centric_Platform.Application.Dtos.Auth;
namespace Teacher_Centric_Platform.Infrastructure.Identity
{
    /// <summary>
    /// Identity service backed by ASP.NET Identity (UserManager / SignInManager).
    /// Also creates domain entities (Doctor / Nurse) during registration.
    /// </summary>
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IApplicationDbContext _dbContext;
        private readonly IEmailService _emailService;
        private readonly ILogger<IdentityService> _logger;


        public IdentityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IJwtTokenGenerator jwtTokenGenerator,
            IApplicationDbContext dbContext,
            IEmailService emailService,
            ILogger<IdentityService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _dbContext = dbContext;
            _emailService = emailService;
            _logger = logger;
        }

        // ── Login ──────────────────────────────────────────────────────
        public async Task<ResultDto<AuthResponseDto>> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _logger.LogWarning("Authentication failed: No user with {Email}", email);

                return ResultDto<AuthResponseDto>.Failure(
                    "Invalid email or password.");
            }

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                return ResultDto<AuthResponseDto>.Failure(
                    "Account is temporarily locked due to multiple failed login attempts.");
            }

            if (!result.Succeeded)
            {
                _logger.LogWarning("Authentication failed for {Email}", email);

                return ResultDto<AuthResponseDto>.Failure(
                    "Invalid email or password.");
            }


            var roles = (await _userManager.GetRolesAsync(user)).ToList();

            if (!roles.Any())
            {
                _logger.LogWarning(
                    "Authentication warning: User {UserId} ({Email}) has no roles assigned",
                    user.Id, email);
            }

            var token = _jwtTokenGenerator.GenerateToken(
                user.Id,
                user.FullName,
                user.Email!,
                roles);

            var response = new AuthResponseDto
            {
                Token = token,
                Email = user.Email!,
                UserId = user.Id,
                Roles = roles,
            };

            _logger.LogInformation(
                "User {UserId} ({Email}) authenticated successfully",
                user.Id, email);

            return ResultDto<AuthResponseDto>.SuccessResult(
                response,
                "Login successful.");
        }

        // ── Register ───────────────────────────────────────────────────
        public async Task<ResultDto<string>> RegisterAsync(RegisterRequestDto request)
        {
            // 1️⃣ Validate Role
            var role = "Student";

            // 2️⃣ Create Identity User
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                NationalId = request.NationalId,
            };

            var identityResult = await _userManager.CreateAsync(user, request.Password);

            if (!identityResult.Succeeded)
            {
                var errors = string.Join("; ",
                    identityResult.Errors.Select(e => e.Description));

                _logger.LogWarning(
                    "Registration failed for {Email}: {Errors}",
                    request.Email, errors);

                return ResultDto<string>.Failure(errors);
            }

            // 3️⃣ Ensure Role Exists
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            await _userManager.AddToRoleAsync(user, role);

            await _dbContext.SaveChangesAsync(CancellationToken.None);

            _logger.LogInformation(
                "{Role} {UserId} ({Email}) registered successfully",
                role, user.Id, request.Email);

            return ResultDto<string>.SuccessResult(
                user.Id,
                "Account created successfully.");
        }

        // ── Forgot Password (send OTP) ─────────────────────────────────
        public async Task<ResultDto<bool>> SendPasswordResetOtpAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            // ❗ Do NOT reveal whether the email exists
            if (user == null)
            {
                _logger.LogWarning(
                    "Password reset requested for non-existent email {Email}", email);

                return ResultDto<bool>.SuccessResult(
                    true,
                    "If an account with that email exists, a password reset OTP has been sent.");
            }

            var otp = await _userManager.GenerateTwoFactorTokenAsync(
                user,
                TokenOptions.DefaultEmailProvider);

            var subject = "Teacher_Centric — Password Reset OTP";

            var body =
        $@"Your password reset OTP is: {otp}

This code will expire shortly.
If you did not request a password reset, please ignore this email.";

            await _emailService.SendEmailAsync(user.Email!, subject, body);

            _logger.LogInformation("Password reset OTP sent to {Email}", email);

            return ResultDto<bool>.SuccessResult(
                true,
                "If an account with that email exists, a password reset OTP has been sent.");
        }

        // ── Reset Password with OTP ────────────────────────────────────
        public async Task<ResultDto<bool>> ResetPasswordWithOtpAsync(string email, string otp, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _logger.LogWarning(
                    "Password reset attempted for non-existent email {Email}", email);

                return ResultDto<bool>.Failure(
                    "Invalid email or OTP.");
            }

            var isValid = await _userManager.VerifyTwoFactorTokenAsync(
                user,
                TokenOptions.DefaultEmailProvider,
                otp);

            if (!isValid)
            {
                _logger.LogWarning("Invalid OTP for password reset: {Email}", email);

                return ResultDto<bool>.Failure(
                    "Invalid or expired OTP.");
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(
                user,
                resetToken,
                newPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join("; ",
                    result.Errors.Select(e => e.Description));

                _logger.LogWarning(
                    "Password reset failed for {Email}: {Errors}", email, errors);

                return ResultDto<bool>.Failure(errors);
            }

            _logger.LogInformation("Password reset successful for {Email}", email);

            return ResultDto<bool>.SuccessResult(
                true,
                "Password has been reset successfully.");
        }
    }
}
