namespace Ids.SimpleAdmin.Backend.Mappers.Interfaces
{
    public interface IMapper<TContract, TModel>
    {
        TContract ToContract(TModel model);
        TModel ToModel(TContract dto);
        TModel UpdateModel(TModel model, TContract contract);
    }
}
