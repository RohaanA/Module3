using Application.Common.Interfaces.Authentication;
using Domain.Entities;
using Domain.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(IJWTTokenGenerator jWTTokenGenerator, IUnitOfWork unitOfWork)
        {
            _jwtTokenGenerator = jWTTokenGenerator;
            _unitOfWork = unitOfWork;
        }
        public async Task<AuthenticationResult> Login(string email, string password)
        {
            // Check if user exists of given email
            User user = await _unitOfWork.Users.FindByEmailAsync(email);
            if (user is null)
            {
                // Throw 404 error (user not found)
                throw new Exception("User not found");
            }
            string role = "User";
            // Check if user is admin
            IEnumerable<Administrator> adminList = await _unitOfWork.Administrators.FindAsync(a => a.UserID == user.UserID);
            if (adminList.Count() > 0)
                role = "Administrator";

            if (user.Password != password)
            {
                throw new Exception("User password invalid");
            }

            // Generate JWT Token and return success
            Guid userId = Guid.NewGuid();
            var token = _jwtTokenGenerator.GenerateToken(user, role);
            return new AuthenticationResult (
                Guid.NewGuid(),
                user.Name,
                user.Name,
                user.Email,
                token);
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password, string role)
        {
            // Check if user exists 

            // Create user

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
