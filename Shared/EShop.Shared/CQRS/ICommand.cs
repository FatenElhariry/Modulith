
namespace EShop.Shared.CQRS
{
    public interface ICommand<TResponse> : IRequest<TResponse>    
    {
    }

}
