using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Text;

namespace Infotecs.WebApi.Services
{
    public class CustomerUpdateSender : ICustomerUpdateSender
    {
        private readonly string queue = "";
        private readonly string login = "";
        private readonly string password = "";
        private readonly string host = "";
        private readonly string port = "";

        private readonly IConnection con = null;

        public CustomerUpdateSender(IConfiguration conf)
        {
            this.queue = conf.GetSection("RabbitMQSettings").GetSection("QueueName").Value;
            this.login = conf.GetSection("RabbitMQSettings").GetSection("Login").Value;
            this.password = conf.GetSection("RabbitMQSettings").GetSection("Password").Value;
            this.host = conf.GetSection("RabbitMQSettings").GetSection("HostName").Value;
            this.port = conf.GetSection("RabbitMQSettings").GetSection("Port").Value;

            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = this.login;
            factory.Password = this.password;
            Console.WriteLine("port: " + this.port);
            factory.Port = Convert.ToInt32(this.port);
            factory.HostName = this.host;
            factory.VirtualHost = "/";

            this.con = factory.CreateConnection();
        }

        public bool send(string message)
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
                Console.WriteLine("ex => " + ex.Message);
            }
            return true;
        }
    }
}
