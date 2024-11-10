using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthenticationServiceOptions _options;
        public AuthenticationService(IUserRepository userRepository, IOptions<AuthenticationServiceOptions> options) 
        {
            _userRepository = userRepository;
            _options = options.Value;
        }

        public User? UserAuthenticate(CredentialsRequest credentialsRequest)
        {
            if (string.IsNullOrEmpty(credentialsRequest.Username) || string.IsNullOrEmpty(credentialsRequest.Password))
                return null;

            var user = _userRepository.GetByUsername(credentialsRequest.Username);

            if (user == null) return null;

            if (user.Username == credentialsRequest.Username && user.Password == credentialsRequest.Password) return user;

            return null;
        }

        public string Authentication(CredentialsRequest credentialsRequest)
        {
            var user = UserAuthenticate(credentialsRequest);

            if (user == null)
            {
                throw new UnauthorizedException("User authentication failed");
            }

            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));

            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            // ---- Claims ----

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.Id.ToString()));
            claimsForToken.Add(new Claim("given_name", user.Username));
            claimsForToken.Add(new Claim("role", user.Rol.ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
                    _options.Issuer,
                    _options.Audience,
                    claimsForToken,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddHours(1),
                    credentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return tokenToReturn;
        }

        public class AuthenticationServiceOptions
        {
            public const string AuthenticationService = "AuthenticationService";

            public string Issuer { get; set; }
            public string Audience { get; set; }
            public string SecretForKey { get; set; }
        }
    }
}
