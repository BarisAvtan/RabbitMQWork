using RabbitMQ.Client;
using Sheread;
using System.Text;
using System.Text.Json;

var factory = new ConnectionFactory();

factory.Uri = new Uri("amqps://toggqveh:poj3d4VTUAOnf_T8YLTWdApegWH5OOTV@shark.rmq.cloudamqp.com/toggqveh");

using var connection = factory.CreateConnection();

var channel = connection.CreateModel();

channel.ExchangeDeclare("header-exchange", durable: true, type: ExchangeType.Headers);

Dictionary<string, object> headers = new Dictionary<string, object>();

headers.Add("format", "pdf");

headers.Add("shape2", "a4");

var properties = channel.CreateBasicProperties();

properties.Headers = headers;
//properties.Persistent = true;  // dersek mesajlar kalıcı hale gelir 

var product = new Product { Id = 1, Name = "Bilgisayar", Price = 100, Stock = 10 };

var productJsonString = JsonSerializer.Serialize(product);

channel.BasicPublish("header-exchange", string.Empty, properties, Encoding.UTF8.GetBytes(productJsonString));

//channel.BasicPublish("header-exchange", string.Empty, properties, Encoding.UTF8.GetBytes("header mesajım"));

Console.WriteLine("Mesaj Gönderilmiştir");

Console.ReadLine();

public enum LogNames
{
    Critical = 1,
    Error = 2,
    Warning = 3,
    Info = 4
}
