using MediatR;

namespace Contracts.Commons.Events
{
    public abstract class BaseEvent : INotification
    {
        protected BaseEvent()
        {
        }
    }
}