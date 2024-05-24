using Application.DTO;
using Application.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
namespace WebApi.Controllers
{
    public class RabbitMQProjectConsumerController : IRabbitMQConsumerController
    {
        private List<string> _errorMessages = new List<string>();
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private string _queueName;

        public RabbitMQProjectConsumerController(IServiceScopeFactory serviceScopeFactory, IConnectionFactory factory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _factory = factory;
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "project_create", type: ExchangeType.Fanout);

            Console.WriteLine(" [*] Waiting for messages from Project.");
        }

        public void ConfigQueue(string queueName)
        {
            _queueName = "project" + queueName;

            _channel.QueueDeclare(queue: _queueName,
                                            durable: true,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

            _channel.QueueBind(queue: _queueName,
                  exchange: "project_create",
                  routingKey: string.Empty);
        }

        public void StartConsuming()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                ProjectDTO projectDTO = ProjectAmqpDTO.ToDTO(message);
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var projectService = scope.ServiceProvider.GetRequiredService<ProjectService>();

                    await projectService.Add(projectDTO);
                }

                Console.WriteLine($" [x] Received {message}");
            };
            _channel.BasicConsume(queue: _queueName,
                                autoAck: true,
                                consumer: consumer);
        }
    }
}