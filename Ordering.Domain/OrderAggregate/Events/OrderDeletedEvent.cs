using Contracts.Commons.Events;

namespace Ordering.Domain.OrderAggregate.Events
{
    public class OrderDeletedEvent : BaseEvent
    {
        public int Id { get; }

        public OrderDeletedEvent(int id)
        {
            Id = id;
        }
    }
}