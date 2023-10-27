﻿using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQSender
{
    public static class HeaderExchangePublisher
    {
        public static void Publish(IModel channel) {

            var timeToLeave = new Dictionary<string, object>
            {
                {"x-message-ttl",30000 }
            };

            channel.ExchangeDeclare("demo-header-exchange",
                type: ExchangeType.Topic,
                arguments: timeToLeave
                );

            var count = 0;
            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello - {count}" };

                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object> { { "account", "new" } };

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                channel.BasicPublish("demo-header-exchange", string.Empty, properties, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
