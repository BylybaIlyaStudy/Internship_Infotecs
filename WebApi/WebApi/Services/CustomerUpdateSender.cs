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
                channel.QueueDeclare("events", false, false, false, null);
                channel.QueueBind("events", "testex", "events", null);
                var msg = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("testex", "events", null, msg);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return true;
        }
    }
}
