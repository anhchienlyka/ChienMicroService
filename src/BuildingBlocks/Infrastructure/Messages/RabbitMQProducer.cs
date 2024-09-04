
using Contracts.Common.Interfaces;
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
            //tạo connection đến hostname
            var connection = connectionFactory.CreateConnection();
            //tạo 1 channel
            using var channel = connection.CreateModel();

            //khai báo 1 queue
            channel.QueueDeclare("orders", exclusive: false);

            var jsonData = _serializeService.Serialize(message);
            //ma hoa
            var body = Encoding.UTF8.GetBytes(jsonData);

            channel.BasicPublish(exchange: "", routingKey: "orders", body: body);

        }
    }
}
