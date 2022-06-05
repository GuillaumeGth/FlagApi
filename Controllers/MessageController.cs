using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FlagApi.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using NpgsqlTypes;
using System.Collections.Generic;
using FlagApi.SignalR;
namespace FlagApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private DatabaseContext _context;
        private IHttpContextAccessor _contextAccessor;   
        private readonly ILogger<UserController> _logger;
        private ChatHub ChatHub;
        public MessageController(ILogger<UserController> logger, 
        DatabaseContext context, 
        IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _context = context;
            _contextAccessor = contextAccessor;
            ChatHub = new ChatHub();
        }
        [HttpPost]
        [Route("get/chat")]
        public ActionResult Chat(JsonElement arg)
        {             
            try{
                Guid author = Guid.Parse(arg.GetProperty("author").ToString());
                Guid recipient = Guid.Parse(arg.GetProperty("recipient").ToString());                
                List<Message> messages = _context.Messages
                    .Where(m => (m.Author == author && m.Recipient == recipient)
                    || (m.Author == recipient && m.Recipient == author))
                    .OrderBy(m => m.Date)
                    .Take(100)?.ToList() ?? new List<Message>();
                Logger.Log(messages.Count);
                return Ok(messages);       
            }
            catch(Exception e){                
                Logger.Error(e);
            }
            return null;
        }
        [HttpPost]
        [Route("send")]
        public ActionResult Send(JsonElement arg)
        {           
            try{  
                Logger.Log("send message");  
                Logger.Log(arg);                            
                DateTime dateTime = DateTime.Now;        
                Message newMessage = new Message(){
                    Text = arg.GetProperty("content").ToString(),
                    Date = dateTime,
                    Location = new NpgsqlPoint(
                        double.Parse(arg.GetProperty("lat").ToString()), 
                        double.Parse(arg.GetProperty("lon").ToString())),
                    Author = Guid.Parse(arg.GetProperty("author").ToString()),
                    Recipient = Guid.Parse(arg.GetProperty("recipient").ToString())
                };
                _context.Messages.Add(newMessage);
                _context.SaveChanges(); 
            }
            catch(Exception e){
                Logger.Error(e);
                return null;
            }
            return Ok();
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult GetMessages(Guid id){
            try{
                var messages =  _context.Messages
                    .Where(m => m.Recipient == id).ToList();
                Logger.Log(messages);
                return Ok(messages);
            }
            catch(Exception e){
                Logger.Error(e);
                return null;
            }            
        }
    }
}