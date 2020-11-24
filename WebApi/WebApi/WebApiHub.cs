using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infotecs.WebApi
{
    public class WebApiHub : Hub
    {
        [EnableCors("AnotherPolicy")]
        public async Task Send(string message)
        {
            await this.Clients.Caller.SendAsync("Send", "update");
            //await this.Clients.All.SendAsync("Send", message + " resend");
        }

        [EnableCors("AnotherPolicy")]
        public async Task SendAll(string message)
        {
            //await this.Clients.Caller.SendAsync("Send", "update");
            await this.Clients.All.SendAsync("Send", "update");
        }
    }
}
