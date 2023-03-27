using RabbitMQ.Client;
using System.Text;


var factory = new ConnectionFactory();

factory.Uri = new Uri("amqps://toggqveh:poj3d4VTUAOnf_T8YLTWdApegWH5OOTV@shark.rmq.cloudamqp.com/toggqveh");

using var connection = factory.CreateConnection();

var channel = connection.CreateModel();

//1.parametre exchange name
//2.parametre durable: true fiziksek olarak kayıt edilsin rest attıgında bu exchange kayıp olmasın
//3.parametre exchange tipi belirttik
channel.ExchangeDeclare("logs-fanout", durable: true, type: ExchangeType.Fanout);//exchange oluşturduk

Enumerable.Range(1, 50).ToList().ForEach(x =>
{

    string message = $"log {x}";

    var messageBody = Encoding.UTF8.GetBytes(message);
    //1.parametre exchange adı
    //2.paramtre kuyruk ismi yok
    //3.parametre 
    //4.parametre
    channel.BasicPublish("logs-fanout", "", null, messageBody);

    Console.WriteLine($"Mesaj gönderilmiştir : {message}");

});



Console.ReadLine();
