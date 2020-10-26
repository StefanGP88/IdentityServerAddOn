using Ids.SimpleAdmin.Backend.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers.Interfaces
{
    public interface IUserHandler
    {
        Task CreateUser(CreateUserRequestDto dto);
        Task UpdateUser(UpdateUserRequestDto dto);
        Task DeleteUser(string userId);
        Task<UserResponseDto> ReadUser(string userId);
        Task<ListDto<UserResponseDto>> ReadAllUsers(int page, int pagesize, CancellationToken cancel);
    }
}
