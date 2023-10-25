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
    public static class DirectExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("demo-direct-exchange", type: ExchangeType.Direct);
            channel.QueueDeclare("demo-direct-queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

            channel.QueueBind("demo-direct-queue", "demo-direct-exchange", "account.init");
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, args) =>
            {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume("demo-direct-queue", true, consumer);
            Console.WriteLine("Consumer Started");
            Console.Read();
        }
    }
}
