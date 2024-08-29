using Contracts.Commons.Interfaces;
using Contracts.Domains;

namespace Contracts.Commons.Events
{
    public class EventEntity<T> : EntityBase<T>, IEventEntity<T>
    {
        private readonly List<BaseEvent> _domainEvents = new();

        public void AddDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvent()
        {
            _domainEvents.Clear();
        }

        //sau khi lay ve list event thi khong cho sua
        public IReadOnlyCollection<BaseEvent> DomainEvents()
        {
            return _domainEvents.AsReadOnly();
        }

        public void RemoveDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }
    }
}