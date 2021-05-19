using Ids.SimpleAdmin.Backend.Mappers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ids.SimpleAdmin.Backend.Mappers
{
    public abstract class AbstractMapper<TContract, TModel> : IMapper<TContract, TModel>
    {
        public abstract TContract ToContract(TModel model);
        public abstract TModel ToModel(TContract dto);
        public abstract TModel UpdateModel(TModel model, TContract contract);
        public TModel AddOrUpdateToList( TContract contract,List<TModel> modelList, Func< TModel,  bool> predicate)
        {
                var m = modelList.SingleOrDefault(predicate);
                if (m is null)
                    return ToModel(contract);
                return UpdateModel(m, contract);
        }
    }
}
