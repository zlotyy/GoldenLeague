using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GoldenLeague.Utils
{
    public interface IJwtAuthenticationManager
    {
        string CreateToken(string userId);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }

    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly AppSettings _settings;
        private const int EXPIRES_MINUTES = 60;

        public JwtAuthenticationManager(IOptions<AppSettings> options)
        {
            _settings = options.Value;
        }

        public string CreateToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = GetSecretKey();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId)
                }),
                Expires = DateTime.Now.AddMinutes(EXPIRES_MINUTES),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var key = GetSecretKey();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false, //here we are saying that we don't care about the token's expiration date
                //ClockSkew = TimeSpan.Zero // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        private byte[] GetSecretKey()
        {
            return Encoding.ASCII.GetBytes(_settings.Secret);
        }
    }
}
