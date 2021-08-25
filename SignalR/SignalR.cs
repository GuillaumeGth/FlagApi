using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using FlagApi.Models;
using Newtonsoft;
using System.Text.Json;
namespace FlagApi.SignalR
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(JsonElement args)
        {          
            string content = args.GetProperty("content").ToString();             
            Logger.Log("signalR SendMessage"); 
            Logger.Log(args.GetType());            
            await Clients?.All.SendAsync("ReceiveMessage", content);
        }
    }
}