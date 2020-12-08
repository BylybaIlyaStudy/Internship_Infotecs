using RabbitMQ.Client;

namespace Infotecs.WebApi.Services
{
    public interface ICustomerUpdateSender
    {
        public bool send(string message);
    }
}
