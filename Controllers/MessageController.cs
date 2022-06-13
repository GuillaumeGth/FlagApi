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
using System.IO;
using Microsoft.AspNetCore.Hosting;
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
        private IWebHostEnvironment _environment;        
        public MessageController(ILogger<UserController> logger, 
        DatabaseContext context, 
        IHttpContextAccessor contextAccessor,
        IWebHostEnvironment environment)
        {
            _logger = logger;
            _context = context;
            _contextAccessor = contextAccessor;
            _environment = environment;
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
                    .Where(m => (m.AuthorId == author && m.RecipientId == recipient)
                    || (m.AuthorId == recipient && m.RecipientId == author))
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
        public ActionResult Send([FromForm] Message arg)
        {           
            try{ 
                Logger.Log("send message");   
                var files = this.HttpContext.Request.Form.Files;
                if (files.Count > 0){
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "Files");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }  
                    var file = files[0];                                       
                    string filePath = Path.Combine(path, file.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        file.CopyTo(fileStream);
                    }                                     
                    Content c = new Content() {
                        ContentPath = filePath,
                        ContentName = file.FileName                
                    };
                    _context.Contents.Add(c);
                    arg.ContentId = c.Id;
                }                                                      
                DateTime dateTime = DateTime.Now;
                Message newMessage = new Message(){
                    Text = arg.Text,
                    Date = dateTime,                    
                    Location = new NpgsqlPoint(
                        arg.Latitude, 
                        arg.Longitude),
                    AuthorId = arg.AuthorId,
                    RecipientId = arg.RecipientId,
                    ContentId = arg.ContentId
                };
                Logger.Log(newMessage);
                _context.Messages.Add(newMessage);
                _context.SaveChanges();
                return Ok(newMessage);
            }
            catch(Exception e){                
                Logger.Error(e);
                return null;
            }
        }
        [HttpGet]
        [Route("user/{id}")]
        public ActionResult GetMessages(Guid id){
            try{                  
                var messages =  _context.Messages
                    .Where(m => m.RecipientId == id).ToList();
                messages.ForEach(m => {                    
                    Logger.Log(m);
                });       
                return Ok(messages);
            }
            catch(Exception e){
                Logger.Error(e);
                return null;
            }            
        }
        [HttpGet]
        [Route("flag/{id}")]
        public ActionResult GetFlag([FromForm] Message message){
            try{
                var m =  _context.Messages
                    .First(m => m.Id == message.Id);

                Logger.Log(m);
                return Ok(m);
            }
            catch(Exception e){
                Logger.Error(e);
                return null;
            }            
        }
        [HttpGet]
        [Route("opended/{id}")]
        public ActionResult ChangeFlagStatus(Guid id){
            try{
                Logger.Log("opened");
                var message =  _context.Messages
                    .First(m => m.Id == id);
                message.Seen = true;
                _context.Update(message);
                _context.SaveChanges();                
                return Ok(message);
            }
            catch(Exception e){
                Logger.Error(e);
                return null;
            }            
        }
    }
}