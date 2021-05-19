using System;
using System.Collections.Generic;

namespace Ids.SimpleAdmin.Backend.Mappers.Interfaces
{
    public interface IMapper<TContract, TModel>
    {
        TContract ToContract(TModel model);
        TModel ToModel(TContract dto);
        TModel UpdateModel(TModel model, TContract contract);
        TModel AddOrUpdateToList(TContract contract, List<TModel> modelList, Func<TModel, bool> predicate);
    }
}
