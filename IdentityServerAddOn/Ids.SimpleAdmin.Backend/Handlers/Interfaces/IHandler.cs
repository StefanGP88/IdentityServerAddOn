using Ids.SimpleAdmin.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers.Interfaces
{
    public interface IHandler<T>
    {
        Task<ListDto<T>> GetAll(int page, int pageSize, CancellationToken cancel);
        Task<ListDto<T>> Delete<T2>(T2 id, int page, int pageSize, CancellationToken cancel);
        Task<T> Create(T dto, int page, int pageSize, CancellationToken cancel);
    }
}
