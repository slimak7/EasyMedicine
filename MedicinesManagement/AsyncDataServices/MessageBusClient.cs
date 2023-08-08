using MedicinesManagement.Dtos;
using RabbitMQ.Client;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace MedicinesManagement.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;

            string sectionName = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "DockerDepl" : "Dev";

            var factory = new ConnectionFactory() 
            {
                HostName = _configuration.GetSection(sectionName)["RabbitMQHost"],
                Port = int.Parse(_configuration.GetSection(sectionName)["RabbitMQPort"])
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                _connection.ConnectionShutdown += RabbitMQConnectionShutdown;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not connect to the message bus || " + ex.ToString());
            }
        }

        private void RabbitMQConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine("RabbitMQ connection shot down");
        }

        public void PublishNewLeaflet(MedicineUpdateInfoDto newLeaflet)
        {
            var message = JsonSerializer.Serialize(newLeaflet);

            if (_connection.IsOpen)
            {
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("RabbitMQ connection is closed");
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);


        }

        public void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
    }
}
