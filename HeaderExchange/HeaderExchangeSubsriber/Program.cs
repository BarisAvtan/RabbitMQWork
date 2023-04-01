using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Sheread;
using System.Text;
using System.Text.Json;

var factory = new ConnectionFactory();

factory.Uri = new Uri("amqps://toggqveh:poj3d4VTUAOnf_T8YLTWdApegWH5OOTV@shark.rmq.cloudamqp.com/toggqveh");

using var connection = factory.CreateConnection();

var channel = connection.CreateModel();

channel.ExchangeDeclare("header-exchange", durable: true, type: ExchangeType.Headers);

channel.BasicQos(0, 1, false);

var consumer = new EventingBasicConsumer(channel);

var queueName = channel.QueueDeclare().QueueName;

Dictionary<string, object> headers = new Dictionary<string, object>();

headers.Add("format", "pdf");
headers.Add("shape", "a4");
headers.Add("x-match", "any");
//headers.Add("x-match", "all");//2.parametre all ise key ve value seçeneklerinin kesinlikle eşleşmesi gerekir.

channel.QueueBind(queueName, "header-exchange", String.Empty, headers);

channel.BasicConsume(queueName, false, consumer);

Console.WriteLine("Logları dinleniyor...");

consumer.Received += (object sender, BasicDeliverEventArgs e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());

    Product product = JsonSerializer.Deserialize<Product>(message);

    Thread.Sleep(1500);

    Console.WriteLine($"Gelen Mesaj: {product.Id}-{product.Name}-{product.Price}-{product.Stock}");

    channel.BasicAck(e.DeliveryTag, false);
};

Console.ReadLine();