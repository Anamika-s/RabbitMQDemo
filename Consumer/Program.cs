using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {

            var factory = new ConnectionFactory
            {
                Uri
                = new Uri("amqp://guest:guest@http://13.235.79.105:5672")
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare("demoQueue", durable: true, exclusive: false,
                autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
             {
                 var body = e.Body.ToArray();
                 var message = Encoding.UTF8.GetString(body);
                 Console.WriteLine(message);
                 channel.BasicConsume("demoqueue", true, consumer);
             };
             }
         
    }
}
