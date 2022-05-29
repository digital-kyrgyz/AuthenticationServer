using System;
using System.Security.Cryptography;
using Core.Configuration;
using Core.Dtos;
using Core.Entities;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SharedLibrary.Configurations;

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