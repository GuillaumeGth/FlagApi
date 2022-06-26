using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FlagApi.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using NpgsqlTypes;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using FlagApi.Services;
using System.Threading.Tasks;

namespace FlagApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private DatabaseContext _context;
        private IHttpContextAccessor _contextAccessor;   
        private readonly ILogger<UserController> _logger;        
        private IWebHostEnvironment _environment;        
        public MessageController(ILogger<UserController> logger, 
        DatabaseContext context, 
        IHttpContextAccessor contextAccessor,
        IWebHostEnvironment environment, 
        INotificationService notificationService)
        {
            _notificationService = notificationService;
            _logger = logger;
            _context = context;
            _contextAccessor = contextAccessor;
            _environment = environment;            
        }        
        [HttpPost]
        [Route("send")]
        public async Task<ActionResult> Send([FromForm] Message arg)
        {           
           
            try{                
                _logger.LogInformation("send message");   
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
                newMessage.Recipient = _context.Users.First(u => u.Id == arg.RecipientId);
                newMessage.Author = _context.Users.First(u => u.Id == arg.AuthorId);
                _logger.LogInformation(newMessage.ToString());
                _context.Messages.Add(newMessage);
                _context.SaveChanges();
                
                if (_notificationService == null)
                    _logger.LogWarning("_notificationService is null");
                else {
                    _logger.LogWarning("_notificationService is not null");
                    _logger.LogInformation(newMessage.Recipient.ToString());
                }
                var notif = new NotificationModel() { 
                    Title = "New Message",
                    Body = $"From {newMessage.Author.Name}",
                    DeviceId = newMessage.Recipient.DeviceId,
                    IsAndroiodDevice = true
                };
                notif.Data["location"] = newMessage.Location;
                var result = await _notificationService.SendNotification(notif);  
                newMessage.Author = null;
                newMessage.Recipient = null;          
                return Ok(newMessage);
            }
            catch(Exception e){                
                _logger.LogError(e.ToString());
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
                    _logger.LogInformation(m.ToString());
                });       
                return Ok(messages);
            }
            catch(Exception e){
                _logger.LogError(e.ToString());
                return null;
            }            
        }
        [HttpGet]
        [Route("flag/{id}")]
        public ActionResult GetFlag([FromForm] Message message){
            try{
                var m =  _context.Messages
                    .First(m => m.Id == message.Id);

                _logger.LogInformation(m.ToString());
                return Ok(m);
            }
            catch(Exception e){
                _logger.LogError(e.ToString());
                return null;
            }            
        }
        [HttpGet]
        [Route("opended/{id}")]
        public ActionResult ChangeFlagStatus(Guid id){
            try{
                _logger.LogInformation("opened");
                var message =  _context.Messages
                    .First(m => m.Id == id);
                message.Seen = true;
                _context.Update(message);
                _context.SaveChanges();                
                return Ok(message);
            }
            catch(Exception e){
                _logger.LogError(e.ToString());
                return null;
            }            
        }
        [HttpGet]
        [Route("chat/{author}/{recipient}")]
        public ActionResult GetChat(Guid author, Guid recipient){
            _logger.LogInformation("GET CHAT");
            var messages = _context.Messages.Where(m => (m.AuthorId == author && m.RecipientId == recipient) 
            || (m.AuthorId == recipient && m.RecipientId == author)).OrderBy(m => m.Date);            
            _logger.LogInformation(messages.ToString());
            return Ok(messages);            
        }
    }
}