using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infotecs.WebApi.Services
{
    public class CustomerUpdateSender
    {
        private readonly string queue = Program.Configuration.GetSection("RabbitMQSettings").GetSection("QueueName").Value;
        public IConnection GetConnection()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = "test";
            factory.Password = "test";
            factory.Port = 8081;
            factory.HostName = "localhost";
            factory.VirtualHost = "/";

            return factory.CreateConnection();
        }
        public bool send(IConnection con, string message)
        {
            try
            {
                IModel channel = con.CreateModel();
                //channel.ExchangeDeclare("", ExchangeType.Direct);
                channel.QueueDeclare(queue, false, false, false, null);
                channel.QueueBind(queue, "testex", queue, null);
                var msg = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("testex", queue, null, msg);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return true;
        }
    }
}
