
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using RabbitMQ.Client;
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
channel.QueueDeclare("demo-queue",
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null);

for (int i = 0; i < 100; i++)
{
    var message = new { Name = "Producer", Message = $"Hello - {i}" };

    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
    channel.BasicPublish("", "demo-queue", null, body);

    Task.Delay(1000).Wait();
}