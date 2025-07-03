namespace EShop.Shared.Contract.CQRS
{
    public interface IQuery<out T> : IRequest<T> where T : notnull
    {
    }
}
