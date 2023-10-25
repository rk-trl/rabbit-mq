


using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQReceiver;
using System.Text;


/*
 * First example
 */
//var factory = new ConnectionFactory { HostName = "localhost" };

//using(var connection = factory.CreateConnection())
//{

//    //channel
//    using(var channel = connection.CreateModel())
//    {
//        channel.QueueDeclare("myQueue", false, false, false, null);

//        //craete a consumer
//        var consumer = new EventingBasicConsumer(channel);

//        //Handle received message
//        consumer.Received += (model, ea) =>
//        {
//            var body = ea.Body;
//            var msg = System.Text.Encoding.UTF8.GetString(body.ToArray());
//            Console.WriteLine("Message Received {0}", msg);
//        };
//        //start consuming
//        channel.BasicConsume("myQueue",true,consumer);
//        Console.WriteLine("Listening messages. press any key to stop listening");
//        Console.ReadLine();

//    }
//}



/*
 * Second example
 */
var factory = new ConnectionFactory { Uri = new Uri("amqp://guest:guest@localhost:5672") };
var connection = factory.CreateConnection();
var channel = connection.CreateModel();
DirectExchangeConsumer.Consume(channel);