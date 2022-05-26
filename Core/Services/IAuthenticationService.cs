using System.Threading.Tasks;
using Core.Dtos;
using SharedLibrary.Dto;

namespace Core.Services
{
    public interface IAuthenticationService
    {
        Task<ResponseDto<TokenDto>> CreateToken(LoginDto loginDto);
        Task<ResponseDto<TokenDto>> CreateTokenByRefreshToken(string refreshToken);
        Task<ResponseDto<NoDataDto>> RevokeRefreshToken(string refreshToken);
        Task<ResponseDto<ClientTokenDto>> CreateTokenByClient(ClientLoginDto clientLoginDto);
    }
}