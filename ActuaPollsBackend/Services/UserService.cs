using ActuaPollsBackend.Helpers;
using ActuaPollsBackend.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ActuaPollsBackend.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly PollsContext _pollsContext;

        public UserService(IOptions<AppSettings> appSettings, PollsContext memberContext)
        {
            _appSettings = appSettings.Value;
            _pollsContext = memberContext;
        }

        public User Authenticate(string email, string password)
        {
            var user = _pollsContext.Users.SingleOrDefault(x => x.Email == email && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserID", user.UserID.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("Username", user.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;
            return user;
        }

        public String Register(string username, string email, string password)
        {
            var usernameCheck = _pollsContext.Users.SingleOrDefault(x => x.Username == username);

            if (usernameCheck != null)
            {
                return "username";
            }

            var emailCheck = _pollsContext.Users.SingleOrDefault(x => x.Email == email);

            if (emailCheck != null)
            {
                return "email";
            }

            User newUser = new User { Username = username, Password = password, Email = email };
            _pollsContext.Users.Add(newUser);
            _pollsContext.SaveChangesAsync();

            return "succes";
        }
    }
}
