using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AuthApi.Data;
using AuthApi.Models;
using AuthApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AuthApi.Services.Implementations
{
    public class IdentityService : IIdentityService
    {
        private readonly IConfiguration _configuration;
        private readonly IdentityDbContext _identityDb;
        private readonly ILogger<IdentityService> _logger;
        public IdentityService(
            IConfiguration configuration, 
            IdentityDbContext identityDb, 
            ILogger<IdentityService> logger)
        {
            _configuration = configuration;
            _identityDb = identityDb;
            _logger = logger;
        }

        public string GetToken(User user)
        {
            List<Claim> claimsForToken = new List<Claim>{
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claimsForToken,
                issuer: _configuration.GetSection("Jwt:Issuer").Value,
                audience: _configuration.GetSection("Jwt:Audience").Value,
                signingCredentials: credentials,
                expires: DateTime.Now.AddHours(6)
            );
            string jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public User? GetUser(UserFormInfo userForm)
        {
            User? user = null;
            User userBuilder = new User();
            userBuilder.Name = userForm.Name;
            userBuilder.HashedPassword = HashPassword(userForm.Password);
            try
            {
                user = _identityDb.Users.Where(u => u.Name == userBuilder.Name && u.HashedPassword == userBuilder.HashedPassword).First();
            }
            catch (Exception)
            {
                _logger.LogWarning("User not found");
                return null;
            }
            return user;
        }

        public string? RegisterUser(UserFormInfo userFormInfo)
        {
            if(userFormInfo.Name == null && userFormInfo.Password == null)
            {
                _logger.LogError("user name or password is empty");
                return null;
            }

            User? user = new User();
            user.Name = userFormInfo.Name!;
            user.HashedPassword = HashPassword(userFormInfo.Password);

            _identityDb.Users.Add(user);
            _identityDb.SaveChanges();

            User userInDb = _identityDb.Users.Where(u=>u.Name == user.Name && u.HashedPassword == user.HashedPassword).First();
            return GetToken(userInDb);
        }

        private  string HashPassword(string password)
        {
            SHA256 sha = SHA256.Create();
            byte[] passwordInByte = Encoding.Unicode.GetBytes(password);
            byte[] hashedPassword = sha.ComputeHash(passwordInByte);
            return Convert.ToBase64String(hashedPassword);
        }
    }
}