using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQReceiver
{
    public static class HeaderExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("demo-header-exchange", type: ExchangeType.Topic);
            channel.QueueDeclare("demo-header-queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

            var header = new Dictionary<string, object> { { "account", "new" } };

            //Not passing routing key
            channel.QueueBind("demo-header-queue", "demo-header-exchange", string.Empty, arguments: header);
            channel.BasicQos(0, 10, false);

            

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, args) =>
            {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume("demo-header-queue", true, consumer);
            Console.WriteLine("Consumer Started");
            Console.Read();
        }
    }
}
