using ActiveSubstancesManagement.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Runtime.InteropServices;
using System.Text;

namespace ActiveSubstancesManagement.AsyncDataServices
{
    public class MessageBusClient : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IEventProcessing _eventProcessing;
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;

        public MessageBusClient(IConfiguration configuration, IEventProcessing eventProcessing)
        {
            _configuration = configuration;
            _eventProcessing = eventProcessing;

            InitializeConnection();
        }

        private void InitializeConnection()
        {
            string sectionName = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "DockerDepl" : "Dev";

            var factory = new ConnectionFactory()
            {
                HostName = _configuration.GetSection(sectionName)["RabbitMQHost"],
                Port = int.Parse(_configuration.GetSection(sectionName)["RabbitMQPort"])
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(
                exchange: "trigger",
                type: ExchangeType.Fanout);
            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _queueName, exchange: "trigger", routingKey: "");

            _connection.ConnectionShutdown += ConnectionShutdown;
        }

        private void ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine("Rabbit MQ connection shotdown");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ModuleHandle, ea) =>
            {
                Console.WriteLine("Event received");

                var body = ea.Body;

                var message = Encoding.UTF8.GetString(body.ToArray());

                _eventProcessing.ProcessEvent(message);
            };

            _channel.BasicConsume(queue: _queueName, consumer: consumer);
            return Task.CompletedTask;
        }

        public override void Dispose() 
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }

            base.Dispose();
        }
    }
}
