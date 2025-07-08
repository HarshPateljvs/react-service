using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using React.Domain.DTOs.Jwt;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace React.DAL.Implementation.Jwt
{
    public class JwtTokenGenerator
    {
        private readonly JwtSettings _settings;

        public JwtTokenGenerator(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
        }

        //public string GenerateToken(Domain.Models.User.User user)
        //{
        //    var claims = new[]
        //    {
        //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //    new Claim(ClaimTypes.Email, user.Email),
        //    new Claim("name", user.Name)
        //};

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        _settings.Issuer,
        //        _settings.Audience,
        //        claims,
        //        expires: DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes),
        //        signingCredentials: creds);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
        public string GenerateToken(Domain.Models.AppUser.AppUser user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("name", user.FirstName +user.LastName)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _settings.Issuer,
                _settings.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
