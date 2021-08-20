using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using FlagApi.Models;

namespace FlagApi.SignalR
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message)
        {
            await Clients.User(message.Destinator.ToString()).SendAsync("ReceiveMessage", message, "testFlag");
        }
    }
}