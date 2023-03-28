
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();
//factory.Uri = new Uri("amqps://uhshoatb:4tRfDsemduk6BCrsZaIvfQgOhLsMtf-t@fish.rmq.cloudamqp.com/uhshoatb");
factory.Uri = new Uri("amqps://toggqveh:poj3d4VTUAOnf_T8YLTWdApegWH5OOTV@shark.rmq.cloudamqp.com/toggqveh");

using var connection = factory.CreateConnection();

var channel = connection.CreateModel();

var randomQueueName = channel.QueueDeclare().QueueName;


//**************
//var randomQueueName2 = "log-database-save-queu";
////2 paramtre true ise fiziksel diseke kuyruk kayıt ediler yani kuyruk silinmez
////3. parametre başka kanallardan bu kuyruga baglanılsınmı
////4.paramtere kuyruk silinsin mi ?
//channel.QueueDeclare(randomQueueName2, true,false); //randomQueueName2 kullamak gerekir böyle yaparsak
//***************


//kuyrugu logs-fanout ismide fanout tipindeki exchangemize(fanout example projesindeo oluşturduk) bind ederiz.
channel.QueueBind(randomQueueName, "logs-fanout", "", null);

//channel.QueueDeclare(randomQueueName, true, false, false);//böyle yaparsak bu kuyruk sürekli durur

channel.BasicQos(0, 1, false);//1 er 1 er al

var consumer = new EventingBasicConsumer(channel);

channel.BasicConsume(randomQueueName, false, consumer);//randomQueueName 'i tüket

Console.WriteLine("Logları dinleniyor...");

consumer.Received += (object sender, BasicDeliverEventArgs e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());

    Thread.Sleep(1500);

    Console.WriteLine("Gelen Mesaj:" + message);

    channel.BasicAck(e.DeliveryTag, false);
};

Console.ReadLine();