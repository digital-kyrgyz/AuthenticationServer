using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using Core.Configuration;
using Core.Dtos;
using Core.Entities;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SharedLibrary.Configurations;
using System.IdentityModel.Token.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly CustomTokenOption _tokenOption;

        public TokenService(UserManager<UserApp> userManager, IOptions<CustomTokenOption> tokenOption)
        {
            _userManager = userManager;
            _tokenOption = tokenOption.Value;
        }

        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }

        private IEnumerable<Claim> GetClaim(UserApp userApp, List<string> audiences)
        {
            var userList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userApp.Id),
                new Claim(JwtRegisteredClaimNames.Email, userApp.Email),
                new Claim(ClaimTypes.Email, userApp.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return userList;
        }

        public TokenDto CreateToken(UserApp user)
        {
            throw new System.NotImplementedException();
        }

        public ClientTokenDto CreateTokenByClient(Client client)
        {
            throw new System.NotImplementedException();
        }
    }
}