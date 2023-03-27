using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();

factory.Uri = new Uri("amqps://toggqveh:poj3d4VTUAOnf_T8YLTWdApegWH5OOTV@shark.rmq.cloudamqp.com/toggqveh");

var connection = factory.CreateConnection();

var channel = connection.CreateModel();//kanal oluşturduk

//channel.QueueDeclare("hello-myqueue", true, false, false, null);

var cunsumer = new EventingBasicConsumer(channel);

channel.BasicQos(0, 1, false);//herbir subsriber'a 1 er mesaj gelsin,3. parametre true ise herbir susbribera aynı sayıda veri gider(global deger denir örneğin 2 paremtre 6 ise 3-3 gider).

channel.BasicConsume("hello-myqueue", false, cunsumer);//2.paramtre false ise mesaj dogruda işlense yanlışda işlense kuyrukdan siler true ise silmez dogru işlersem datayı ben haber verince sil


cunsumer.Received += (object? sender, BasicDeliverEventArgs e) =>//mesajı almak için
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());

    Thread.Sleep(1500); //1.5 sn uyut

    Console.WriteLine("Gelen Mesaj " + message);
    
    //2. parametre  false ise sadece ilgili mesahın durumunu rabbitmq ya bildir true ise memoryde işlenmiş ama rabbir mq ya gitmemiş başka mesajlar varsa bunları rabitt mq nun haberdar olmasını sağlar
    channel.BasicAck(e.DeliveryTag, false);
};

Console.ReadLine();
