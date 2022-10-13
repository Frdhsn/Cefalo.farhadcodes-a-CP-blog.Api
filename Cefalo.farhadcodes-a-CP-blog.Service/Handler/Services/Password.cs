using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using Cefalo.farhadcodes_a_CP_blog.Service.Handler.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.Handler.Services
{
    public class Password: IPassword
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Password(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = config;
            _httpContextAccessor = httpContextAccessor;
        }

        /*public string GetLoggedInEmail()
        {
            var name = String.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                name = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
                return name;
            }
            return name;
        }*/
        public int GetLoggedInId()
        {
            int Id = -1;
            if (_httpContextAccessor.HttpContext != null)
            {
                Id = Int32.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value);
                //return name;
            }
            return Id;
        }

        public string GetTokenCreationTime()
        {
            var creationTime = String.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                creationTime = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Expiration).Value;
                return creationTime;
            }
            return creationTime;
        }
        public Tuple<byte[], byte[]> HashPassword(string password)
        {
            byte[] passwordSalt, passwordHash;
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            return new Tuple<byte[], byte[]>(passwordSalt, passwordHash);
        }

        public bool VerifyHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, (user.Id).ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("EnvironmentVariable:token").Value));

            var createdCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(50),
                signingCredentials: createdCredential);

            var finalToken = new JwtSecurityTokenHandler().WriteToken(token);
            //var ret = JSON
            //return JsonConvert.SerializeObject(finalToken).ToString();
            return finalToken;
        }
    }
}
