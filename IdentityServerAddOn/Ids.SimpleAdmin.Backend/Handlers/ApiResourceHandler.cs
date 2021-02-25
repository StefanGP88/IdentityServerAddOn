using IdentityServer4.EntityFramework.DbContexts;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers
{
    public class ApiResourceHandler: IHandler<ApiResourceContract>
    {
        private readonly ConfigurationDbContext _confContext;
        public ApiResourceHandler(ConfigurationDbContext configurationDbContext)
        {
            _confContext = configurationDbContext;
        }

        public Task<ListDto<ApiResourceContract>> GetAll(int page, int pageSize, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }
        public Task<ListDto<ApiResourceContract>> Delete<T2>(T2 id, int page, int pageSize, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResourceContract> Create(ApiResourceContract dto, int page, int pageSize, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }
    }
}
