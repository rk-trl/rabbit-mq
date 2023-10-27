using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQSender
{
    public static class FanoutExchangePublisher
    {
        public static void Publish(IModel channel) {

            var timeToLeave = new Dictionary<string, object>
            {
                {"x-message-ttl",30000 }
            };

            channel.ExchangeDeclare("demo-fanout-exchange",
                type: ExchangeType.Fanout,
                arguments: timeToLeave
                );

            var count = 0;
            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello - {count}" };

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                channel.BasicPublish("demo-fanout-exchange", string.Empty, null, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
