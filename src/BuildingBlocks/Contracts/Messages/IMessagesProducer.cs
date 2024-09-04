
namespace Contracts.Messages
{
    public interface IMessagesProducer
    {
        void SendMessage<T>(T message);
    }
}
