using EShop.Shared.Domain;

namespace EShop.Basket.Basket.Features.Models;

public class OutboxMessage : Entity<Guid>
{
    public string Type { get; set; }
    public string Content { get; set; }
    public DateTime OccurendOn { get; set; }
    public DateTime? ProcessedOn { get; set; }
}
