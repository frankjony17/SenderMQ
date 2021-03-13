
using System;
using RabbitMQ.Client;
using System.Text;

namespace SenderMQ.Service
{
    public class Sender
    {
        private readonly string queue;
        private readonly ConnectionFactory factory = new() { HostName = "localhost" };

        public Sender(string queueName)
        {
            this.queue = queueName;
        }


        private void StartQueue(IModel channel)
        {
            channel.QueueDeclare(
                queue: queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
        }

        // ver static???
        private void PublicMessage(IModel channel, string message)
        {
            channel.BasicPublish(
                exchange: "",
                routingKey: queue,
                basicProperties: null,
                body: Encoding.UTF8.GetBytes(message)
            );
        }

        public void SendMessage(string message)
        {
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();  // ver using???
            
            StartQueue(channel);
            PublicMessage(channel, message);

            Console.WriteLine(" [x] Sent {0}", message);
        }
    }
}
