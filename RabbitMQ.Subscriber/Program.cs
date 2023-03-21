
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://toggqveh:poj3d4VTUAOnf_T8YLTWdApegWH5OOTV@shark.rmq.cloudamqp.com/toggqveh");

var connection = factory.CreateConnection();

var channel = connection.CreateModel();//kanal oluşturduk

//channel.QueueDeclare("hello-myqueue", true, false, false, null);

var cunsumer = new EventingBasicConsumer(channel);

channel.BasicConsume("hello-myqueue", true, cunsumer);//2.paramtre false ise mesaj dogruda işlense yanlışda işlense kuyrukdan siler true ise silmez dogru işlersem datayı ben haber verince sil


cunsumer.Received += (object? sender, BasicDeliverEventArgs e) =>//mesajı almak için
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());

    Console.WriteLine("Gelen Mesaj " + message);
};


Console.ReadLine();
