using Application.Common.Interfaces.Authentication;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJWTTokenGenerator _jwtTokenGenerator;

        public AuthenticationService(IJWTTokenGenerator jWTTokenGenerator)
        {
            _jwtTokenGenerator = jWTTokenGenerator;
        }
        public AuthenticationResult Login(string email, string password)
        {
            return new AuthenticationResult (
                Guid.NewGuid(),
                "Rohaan",
                "Atique",
                "rohaanpk3@gmail.com",
                "token");
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            // Check if user exists 

            // Create user
            User newUser = new User(firstName+lastName, email, password);
            // Create JWT Token
            Guid userId = Guid.NewGuid();
            var token = _jwtTokenGenerator.GenerateToken(userId, firstName, lastName);
            return new AuthenticationResult(
                Guid.NewGuid(),
                "Rohaan",
                "Atique",
                "rohaanpk3@gmail.com",
                token);
        }
    }
}
