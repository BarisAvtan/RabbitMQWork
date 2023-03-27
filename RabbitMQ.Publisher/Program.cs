using RabbitMQ.Client;
using System.Text;

Console.WriteLine("Hello, World!");

//publisher dan Subscriber'a mesaj gidecek

var factory = new ConnectionFactory();

factory.Uri = new Uri("amqps://toggqveh:poj3d4VTUAOnf_T8YLTWdApegWH5OOTV@shark.rmq.cloudamqp.com/toggqveh");

var connection = factory.CreateConnection();

var channel = connection.CreateModel();//kanal oluşturduk

//1.parametre kuyruk ismi
//2.parametre durable false ise kuyruklar memoryde turulur ve rabbitmq yeniden başlatılırsa veriler silinir ,true ise kuyrukda veriler durur veriler fiziksel olarak kayıt edilir
//3.parametre bu kuyruga sadece oluşturulmuş olan kanal üzerinden baglan false olursa farklı kanallardan bu kuyruga baglanilabilir
//4.parametre subsriber  giderse kuyruk silinir silinmemesi için false
channel.QueueDeclare("hello-myqueue", true, false, false, null);

channel.ExchangeDeclare("fanout-type", ExchangeType.Fanout, true, false, null);
//channel.ExchangeDeclare("fanout-type", ExchangeType.Fanout, true, false, null);

string message = "hello word";

var messageBody = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(string.Empty, "hello-myqueue", null, messageBody);

Console.WriteLine("Mesaj gönderildi.");

Console.WriteLine("Enumerable.Range Test");

Enumerable.Range(1, 50).ToList().ForEach(x =>
{

    string message = $"Message {x}";

    var messageBody = Encoding.UTF8.GetBytes(message);

    //channel.BasicPublish("fanout-type", string.Empty, null, messageBody);

    Console.WriteLine($"Mesaj gönderilmiştir. = {message}");

});


Console.ReadLine();


