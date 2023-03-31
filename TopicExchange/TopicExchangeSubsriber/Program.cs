using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();

factory.Uri = new Uri("amqps://toggqveh:poj3d4VTUAOnf_T8YLTWdApegWH5OOTV@shark.rmq.cloudamqp.com/toggqveh");

using var connection = factory.CreateConnection();

var channel = connection.CreateModel();

channel.BasicQos(0, 1, false);

var consumer = new EventingBasicConsumer(channel);

var queueName = channel.QueueDeclare().QueueName;

//var routekey = "*.Error.*";//iiçinde eror geçenler

//var routekey = "*.*.Warning";//sonu sadece warning olanlar

var routekey = "Info.#";//başı info olsun sonu önemli değil

channel.QueueBind(queueName, "logs-topic", routekey);

channel.BasicConsume(queueName, false, consumer);

Console.WriteLine("Loglar dinleniyor...");

consumer.Received += (object sender, BasicDeliverEventArgs e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());

    Thread.Sleep(1500);

    Console.WriteLine("Gelen Mesaj:" + message);

    File.AppendAllText("log-critical.txt", message+ "\n");

    channel.BasicAck(e.DeliveryTag, false);
};





Console.ReadLine();