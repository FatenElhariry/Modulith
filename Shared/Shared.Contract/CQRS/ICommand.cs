
namespace EShop.Shared.Contract.CQRS
{
    public interface ICommand<TResponse> : IRequest<TResponse>    
    {
    }

}
