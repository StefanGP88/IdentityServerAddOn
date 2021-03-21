using FluentValidation;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend.Validators
{
    public class ClientValidator: AbstractValidator<ClientsContract>
    {
      public  ClientValidator()
        {

        }
    }
}
