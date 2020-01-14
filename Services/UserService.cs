using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SportsStoreApi.Entities;
using SportsStoreApi.Helpers;
using SportsStoreApi.Interfaces;

namespace SportsStoreApi.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly StoreContext _storeContext;

        public UserService(StoreContext storeContext, IOptions<AppSettings> appSettings)
        {
            if(storeContext == null)
            {
                throw new ArgumentNullException(nameof(storeContext));
            }
            _appSettings = appSettings.Value;
            this._storeContext = storeContext;
            // this call is required for the table to be created
            storeContext.Database.EnsureCreated();
        }

        public AuthenticatedUserResponse GetById(int id)
        {
            var user = _storeContext.Users.FirstOrDefault(x => id == x.Id);
            if (user == null)
            {
                return null;
            }
            return user.WithoutPassword();
        }

        public AuthenticatedUserResponse Authenticate(string email, string password)
        {
            var user = _storeContext.Users.FirstOrDefault(user => user.Email == email);
            if (user == null || !Auth.PasswordHasher.Check(user.PasswordHash, password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public IEnumerable<AuthenticatedUserResponse> GetAll()
        {
            return _storeContext.Users.WithoutPassword();
        }
    }
}
