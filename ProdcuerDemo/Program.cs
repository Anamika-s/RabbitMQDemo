using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace ProdcuerDemo
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
                var message = new { Name = "Producer", Message = "Hello" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                channel.BasicPublish("", "demoQueue", null, body);
            }
        }
    }

     