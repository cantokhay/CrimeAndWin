using MassTransit.Transports.Fabric;
using Notification.Application.Messaging.Abstract;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Notification.Application.Messaging.Concrete
{
    public class RabbitMqEventPublisher 
        //: IEventPublisher
    {
        private readonly ConnectionFactory _factory;

        public RabbitMqEventPublisher(string hostName = "localhost", string userName = "guest", string password = "guest")
        {
            _factory = new ConnectionFactory()
            {
                HostName = hostName,
                UserName = userName,
                Password = password
            };
        }

        //public Task PublishAsync<T>(T @event, string routingKey)
        //{
        //    using var connection = _factory.CreateConnection();
        //    using var channel = connection.CreateModel();

        //    var exchange = "game.events";
        //    channel.ExchangeDeclare(exchange, ExchangeType.Topic, durable: true);

        //    var message = JsonSerializer.Serialize(@event);
        //    var body = Encoding.UTF8.GetBytes(message);

        //    channel.BasicPublish(exchange, routingKey, null, body);
        //    return Task.CompletedTask;
        //}
    }
}
