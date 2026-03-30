using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Notification.Application.Messaging.Concrete
{
    public class RabbitMqSubscriber 
        //: BackgroundService
    {
        private readonly IMediator _mediator;
        private IConnection _connection;
        private IModel _channel;

        //public RabbitMqSubscriber(IMediator mediator)
        //{
        //    var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
        //    _connection = factory.CreateConnection();
        //    _channel = _connection.CreateModel();

        //    _channel.ExchangeDeclare("game.events", ExchangeType.Topic, durable: true);
        //    _channel.QueueDeclare("notification-service", durable: true, exclusive: false, autoDelete: false);
        //    _channel.QueueBind("notification-service", "game.events", "energy.changed");

        //    _mediator = mediator;
        //}

        //protected override Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    var consumer = new EventingBasicConsumer(_channel);
        //    consumer.Received += async (_, ea) =>
        //    {
        //        var json = Encoding.UTF8.GetString(ea.Body.ToArray());
        //        var energyEvent = JsonSerializer.Deserialize<EnergyChangedEvent>(json);

        //        if (energyEvent != null && energyEvent.Current == energyEvent.Max)
        //        {
        //            await _mediator.Send(new CreateNotificationCommand(
        //                energyEvent.PlayerId,
        //                "Enerjin doldu!",
        //                "Enerjin maksimum seviyeye ulaştı.",
        //                "EnergyFull"
        //            ));
        //        }

        //        _channel.BasicAck(ea.DeliveryTag, false);
        //    };

        //    _channel.BasicConsume("notification-service", false, consumer);
        //    return Task.CompletedTask;
        //}
    }

}
