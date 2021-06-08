using Ids.SimpleAdmin.Backend.Mappers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ids.SimpleAdmin.Backend.Mappers
{
    public abstract class AbstractMapper<TContract, TModel> : IMapper<TContract, TModel>
        where TContract : class
        where TModel : class
    {
        public abstract TContract ToContract(TModel model);
        public abstract TModel ToModel(TContract contract);
        public abstract TModel UpdateModel(TModel model, TContract contract);
        public List<TModel> UpdateList(List<TModel> modelList, List<TContract> contractList)
        {
            var contractProperty = typeof(TContract).GetProperty("Id");
            if (contractProperty is null) throw new Exception("property Id not found");

            var modelProperty = typeof(TModel).GetProperty("Id");
            if (modelProperty is null) throw new Exception("property Id not found");

            return contractList?.ConvertAll(c =>
            {
                var contractId = contractProperty.GetValue(c);
                var model = modelList?.SingleOrDefault(m =>
                {
                    var modelId = modelProperty.GetValue(m);
                    return modelId.Equals(contractId);
                });
                if (model is null)
                    return ToModel(c);
                return UpdateModel(model, c);
            });
        }
    }
}
