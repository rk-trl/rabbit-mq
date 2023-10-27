
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQSender;
using System.Text;
using System.Text.Json.Serialization;

/*
 * First example
 */

//var factory = new ConnectionFactory { HostName = "localhost" };

//using(var connection = factory.CreateConnection())
//{
//    using(var channel = connection.CreateModel())
//    {
//        string msg = string.Empty;
//        //Queue
//        channel.QueueDeclare("myQueue", false, false, false, null);
//        do
//        {
//            var body = System.Text.Encoding.UTF8.GetBytes(msg);

//            //publish message to the queue
//            channel.BasicPublish("", "myQueue", null, body);
//            Console.WriteLine("Message Sent");

//            msg = Console.ReadLine();

//        } while (!string.IsNullOrEmpty(msg));

//    }
//}

/*
 * Second example
 */
var factory = new ConnectionFactory { Uri = new Uri("amqp://guest:guest@localhost:5672") };
var connection = factory.CreateConnection();
var channel = connection.CreateModel();
//DirectExchangePublisher.Publish(channel);
//TopicExchangePublisher.Publish(channel);
FanoutExchangePublisher.Publish(channel);