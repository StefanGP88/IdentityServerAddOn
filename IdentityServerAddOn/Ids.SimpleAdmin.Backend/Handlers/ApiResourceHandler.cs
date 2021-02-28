using IdentityServer4.EntityFramework.DbContexts;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers
{
    public class ApiResourceHandler: IHandler<ApiResourceContract,int>
    {
        private readonly ConfigurationDbContext _confContext;

        private List<ApiResourceContract> tempList = new List<ApiResourceContract>();

        //public ApiResourceHandler(ConfigurationDbContext configurationDbContext)
        //{
        //    _confContext = configurationDbContext;
        //}
        public ApiResourceHandler()
        {
        }

        public async Task<ListDto<ApiResourceContract>> GetAll(int page, int pageSize, CancellationToken cancel)
        {
            var list = new ListDto<ApiResourceContract>();
            list.Items = tempList;
            list.Page = page;
            list.PageSize = pageSize;
            list.TotalItems = tempList.Count;
            return await Task.FromResult(list);
        }
        public async Task<ListDto<ApiResourceContract>> Delete(int id, int page, int pageSize, CancellationToken cancel)
        {
            var _id = int.Parse(id.ToString());
            tempList.RemoveAll(x => x.Id == _id);
            var list = new ListDto<ApiResourceContract>();
            list.Items = tempList;
            list.Page = page;
            list.PageSize = pageSize;
            list.TotalItems = tempList.Count;
            return await Task.FromResult(list);
        }

        public async Task<ApiResourceContract> Create(ApiResourceContract dto, int page, int pageSize, CancellationToken cancel)
        {
            dto.Id = tempList.Count;
            tempList.Add(dto);
            return await  Task.FromResult(dto);
        }
    }
}
