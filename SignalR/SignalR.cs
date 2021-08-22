using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using FlagApi.Models;

namespace FlagApi.SignalR
{
    public class ChatHub : Hub
    {
        public async Task SendMessage()
        {           
            Logger.Log("signalR SendMessage"); 
            Logger.Log(Clients);
            await Clients?.All.SendAsync("ReceiveMessage");
        }
    }
}