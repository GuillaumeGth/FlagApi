using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FlagApi.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using NpgsqlTypes;
using System.Collections.Generic;
namespace FlagApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private DatabaseContext _context;
        private IHttpContextAccessor _contextAccessor;   
        private readonly ILogger<UserController> _logger;
        public MessageController(ILogger<UserController> logger, 
        DatabaseContext context, 
        IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _context = context;
            _contextAccessor = contextAccessor;
        }
        [HttpPost]
        [Route("get/chat")]
        public List<Message> Chat(JsonElement arg)
        { 
            try{

                Guid author = Guid.Parse(arg.GetProperty("author").ToString());
                Guid destinator = Guid.Parse(arg.GetProperty("destinator").ToString());
                Logger.Log(author);
                Logger.Log(destinator);
                List<Message> messages = _context.Messages
                    .Where(m => (m.Author == author && m.Destinator == destinator)
                    || (m.Author == destinator && m.Destinator == author))
                    .OrderBy(m => m.Date)
                    .Take(100)?.ToList() ?? new List<Message>();
                Logger.Log(messages.Count);
                return messages;
            }
            catch(Exception e){
                Logger.Error(e);
            }
            return null;
        }
        [HttpPost]
        [Route("send")]
        public void Send(JsonElement arg)
        {               
            try{                
                
                Logger.Log("send message");
                DateTime dateTime = DateTime.Now;
                // DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                // Logger.Log(arg.GetProperty("date").ToString());
                // dateTime = dateTime.AddSeconds(double.Parse(arg.GetProperty("date").ToString())).ToLocalTime();           
                Message newMessage = new Message(){
                    Text = arg.GetProperty("content").ToString(),
                    Date = dateTime,
                    Location = new NpgsqlPoint(
                        double.Parse(arg.GetProperty("lat").ToString()), 
                        double.Parse(arg.GetProperty("lon").ToString())),
                    Author = Guid.Parse(arg.GetProperty("author").ToString()),
                    Destinator = Guid.Parse(arg.GetProperty("destinator").ToString())
                };
                _context.Messages.Add(newMessage);
                _context.SaveChanges();                
            }
            catch(Exception e){
                Logger.Error(e);
            }
        }
    }
}