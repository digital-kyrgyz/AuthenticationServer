using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Configuration;
using Core.Dtos;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedLibrary.Dto;

namespace Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenService;

        public AuthenticationService(
            IOptions<List<Client>> optionsClient,
            ITokenService tokenService,
            UserManager<UserApp> userManager,
            IUnitOfWork unitOfWork,
            IGenericRepository<UserRefreshToken> userRefreshToken)
        {
            _clients = optionsClient.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenService = userRefreshToken;
        }

        public async Task<ResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return ResponseDto<TokenDto>.Fail("Email or password is wrong", 400, true);
            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return ResponseDto<TokenDto>.Fail("Email or password is wrong", 400, true);
            }

            var token = _tokenService.CreateToken(user);
            var userRefreshToken =
                await _userRefreshTokenService.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();
            if (userRefreshToken == null)
            {
                await _userRefreshTokenService.AddAsync(new UserRefreshToken
                    { UserId = user.Id, Token = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
            }
            else
            {
                userRefreshToken.Token = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }

            await _unitOfWork.CommitAsync();
            return ResponseDto<TokenDto>.Success(token, 200);
        }

        public Task<ResponseDto<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<ResponseDto<NoDataDto>> RevokeRefreshToken(string refreshToken)
        {
            throw new System.NotImplementedException();
        }

        public ResponseDto<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var client = _clients.SingleOrDefault(x =>
                x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);
            if (client == null)
            {
                return ResponseDto<ClientTokenDto>.Fail("Client or clienSecret not found", 404, true);
            }

            var token = _tokenService.CreateTokenByClient(client);
            return ResponseDto<ClientTokenDto>.Success(token, 200);
        }
    }
}