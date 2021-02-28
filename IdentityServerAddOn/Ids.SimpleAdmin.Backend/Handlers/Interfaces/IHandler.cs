using Ids.SimpleAdmin.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers.Interfaces
{
    public interface IHandler<TData, TIdentifier>
    {
        Task<ListDto<TData>> GetAll(int page, int pageSize, CancellationToken cancel);
        Task<ListDto<TData>> Delete(TIdentifier id, int page, int pageSize, CancellationToken cancel);
        Task<TData> Create(TData dto, int page, int pageSize, CancellationToken cancel);
    }
}
