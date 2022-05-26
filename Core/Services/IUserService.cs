using System.Threading.Tasks;
using Core.Dtos;
using SharedLibrary.Dto;

namespace Core.Services
{
    public interface IUserService
    {
        Task<ResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<ResponseDto<UserAppDto>> GetUserByNameAsync(string userName);
    }
}