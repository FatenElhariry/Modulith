using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Shared.DDD
{
    internal interface IAggregate<IId> 
        where IId : notnull
    {
        public List<IDomainEvent> DomainEvents { get; set; }
        public IDomainEvent[] ClearDomainEvents();
    }
}
