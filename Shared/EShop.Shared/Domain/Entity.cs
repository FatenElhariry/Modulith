
namespace EShop.Shared.Domain
{
    public class Entity<TKey> : IEntity where TKey : notnull
    {
        public TKey Id { get; set; } = default!;
        public DateTime? CreatedAt { get ; set ; }
        public DateTime? UpdatedAt { get ; set ; }
        public string? CreatedBy { get ; set ; }
        public string? UpdatedBy { get ; set ; }

        public override bool Equals(object? obj)
        {
            if (obj is Entity<TKey> entity)
            {
                return Id.Equals(entity.Id);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
