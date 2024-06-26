using Application.DTO;
using Application.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
namespace WebApi.Controllers
{
    public class RabbitMQAssociationConsumerController : IRabbitMQConsumerController
    {
        private List<string> _errorMessages = new List<string>();
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private string _queueName;

        public RabbitMQAssociationConsumerController(IServiceScopeFactory serviceScopeFactory, IConnectionFactory factory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _factory = factory;
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();


            _channel.ExchangeDeclare(exchange: "associationCreated", type: ExchangeType.Fanout);

            Console.WriteLine(" [*] Waiting for messages from Association.");
        }

        public void ConfigQueue(string queueName)
        {
            _queueName = "association" + queueName;

            _channel.QueueDeclare(queue: _queueName,
                                            durable: true,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

            _channel.QueueBind(queue: _queueName,
                  exchange: "associationCreated",
                  routingKey: string.Empty);
        }

        public void StartConsuming()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                AssociationDTO associationDTO = AssociationAmqpDTO.Deserialize(message);
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var associationService = scope.ServiceProvider.GetRequiredService<AssociationService>();
                    List<string> errorMessages = new List<string>();
                    await associationService.Add(associationDTO, _errorMessages);
                    if (errorMessages.Any())
                    {
                        Console.WriteLine($"Errors occurred while processing the message: {string.Join(", ", errorMessages)}");
                    }
                    else
                    {
                        Console.WriteLine($"Processed message successfully: {associationDTO}");
                    }
                }

                Console.WriteLine($" [x] Received {message}");
            };
            _channel.BasicConsume(queue: _queueName,
                                autoAck: true,
                                consumer: consumer);
        }
    }
}