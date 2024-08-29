using Contracts.Commons.Interfaces;
using Contracts.Messages;
using RabbitMQ.Client;
using System.Text;

namespace Infrastructure.Messages
{
    public class RabbitMQProducer : IMessagesProducer
    {
        private readonly ISerializeService _serializeService;

        public RabbitMQProducer(ISerializeService serializeService)
        {
            _serializeService = serializeService;
        }

        public void SendMessage<T>(T message)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("orders", exclusive: false);

            var jsonData = _serializeService.Serialize<T>(message);

            // ma hoa
            var body = Encoding.UTF8.GetBytes(jsonData);

            channel.BasicPublish(exchange: "", routingKey: "orders", body: body);
        }
    }
}