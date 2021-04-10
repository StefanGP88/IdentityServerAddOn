using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers
{
    public class RolesHandler : IHandler<RolesContract, string>
    {
        private readonly IdentityDbContext _dbContext;
        public RolesHandler(IdentityDbContext identityDbContext)
        {
            _dbContext = identityDbContext;
        }
        public Task<RolesContract> Create(RolesContract dto, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }

        public Task<ListDto<RolesContract>> Delete(string id, int page, int pageSize, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }

        public Task<RolesContract> Get(string id, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }

        public Task<ListDto<RolesContract>> GetAll(int page, int pageSize, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }

        public Task<RolesContract> Update(RolesContract dto, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }
    }
}
