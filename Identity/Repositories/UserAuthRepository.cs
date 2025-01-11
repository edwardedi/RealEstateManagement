using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Repositories
{
    public class UserAuthRepository : IUserAuthRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public UserAuthRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<Result<string>> Login(User user)
        {
            var existingUser = await context.Users.SingleOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser == null || !BCrypt.Net.BCrypt.Verify(user.PasswordHash, existingUser.PasswordHash))
            {
                return Result<string>.Failure("Invalid email or password");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, existingUser.UserId.ToString()),
                    new Claim("name", existingUser.Name ?? string.Empty),
                    new Claim(ClaimTypes.Email, existingUser.Email ?? string.Empty),
                    new Claim("phone_number", existingUser.PhoneNumber ?? string.Empty)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Result<string>.Success(tokenHandler.WriteToken(token));
        }

        public async Task<Result<Guid>> Register(User user, CancellationToken cancellationToken)
        {
            var existingUser = await context.Users.SingleOrDefaultAsync(u => u.Email == user.Email, cancellationToken);
            if (existingUser != null)
            {
                return Result<Guid>.Failure("Email already in use");
            }

            context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken);
            return Result<Guid>.Success(user.UserId);
        }
    }
}
