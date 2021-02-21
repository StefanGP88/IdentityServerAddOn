using Ids.SimpleAdmin.Backend.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers.Interfaces
{
    public interface IHandler<T>
    {
        Task<ListDto<T>> GetAll(int page, int pageSize, CancellationToken cancel);
        Task Delete<T2>(T2 id);
    }
}
