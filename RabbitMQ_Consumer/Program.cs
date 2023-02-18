using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// Bağlantı Oluşturma.
ConnectionFactory factory = new();
factory.Uri=new("amqps://ajgenuck:uxjM3ZNS4MoDEmnbXOyPtlfixMZZBA4A@shark.rmq.cloudamqp.com/ajgenuck");

//Bağlantı aktifleştirmek ve kanal açmak
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue oluşturma.

channel.QueueDeclare(queue:"example-queue",exclusive:false); // Consumer da kuyruk publisher ile aynı olmalıdır.

// Queue mesaj okuma
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue",false,consumer);
consumer.Received += (sender, e) =>
{
    //Kuyruğa gelen measajların işlendği yerdir.
    // e.Body : kuyruktaki veriler bütünsel olarak gelecektir.
    // e.Body.Span() veya e.Body.Array() : kuruktaki mesajarın byte verisni getirecekit.
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};
Console.ReadLine();

