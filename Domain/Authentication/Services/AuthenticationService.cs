using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Core.Exceptions;
using Domain.Authentication.Enums;
using Domain.Users.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Authentication.Services;

public class AuthenticationService(IConfiguration _configuration) : IAuthenticationService
{
    public string GenerateToken(string userName, UserRole role, int userId)
    {
        var jwtKey = _configuration["App:Jwt:Key"];
        if (string.IsNullOrEmpty(jwtKey))
            throw new ArgumentNullException(nameof(jwtKey), "JWT Key is not configured.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userName),
            new Claim(JwtRegisteredClaimNames.NameId, userId.ToString()),
            new Claim(ClaimTypes.Role, role.ToString()),
            new Claim("UserId", userId.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["App:Jwt:Issuer"],
            audience: _configuration["App:Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(double.Parse(_configuration["App:Jwt:ExpireMinutes"] 
                                                          ?? throw new DomainException("JwtExpireMinutesNotConfigured", 
                                                              (int)AuthenticationErrorCode.JwtExpireMinutesNotConfigured))),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public byte[] ComputePasswordHash(string password, byte[] salt)
    {
        using var hmac = new HMACSHA512(salt);
        return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
}