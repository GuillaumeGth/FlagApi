using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FlagApi.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
namespace FlagApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private DatabaseContext _context;
        private IHttpContextAccessor _contextAccessor;   
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, 
        DatabaseContext context, 
        IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _context = context;
            _contextAccessor = contextAccessor;
        }
         //Retrieve a list of user based on the keyword searched
        [HttpPost]
        [Route("list")]
        public ActionResult List([FromForm] FormData arg)
        {
            try
            {
                string query = arg.Query;
                List<User> users = _context.Users
                    .Where(u => u.Name.ToLower().Contains(query.ToLower()) || u.Email.ToLower().Contains(query.ToLower()))
                    .ToList<User>();
                Logger.Log(users.Count);
                foreach(User u in users){
                    Logger.Log(u);
                }
                return Ok(users);
            }
            catch(Exception e){
                Logger.Error(e);
                return null;
            }
        }

        //Get or create a new user if not exists
        [HttpPost]
        public ActionResult Get([FromForm] User arg)
        {                                    
            bool exists = _context.Users.Any(u => u.Email == arg.Email);
            if (!exists) {                
                Logger.Log("not exists");
                _context.Users.Add(arg);
                _context.SaveChanges();
                Logger.Log(arg);
            }
            else{
                User u = _context.Users.First(u => u.Email == arg.Email);
                Logger.Log(u);
                u.PictureUrl = arg.PictureUrl;
                _context.Users.Update(u);
                _context.SaveChanges();
                return Ok(u.Id);
            }
            return Ok(arg.Id);    
        }
    
         //Retrieve a list of user based on the keyword searched
        [HttpGet]
        [Route("{id}")]
        public ActionResult GetUser(Guid id)
        {
            try{
                return Ok(_context.Users.First(u => u.Id == id));
            }
            catch(Exception e){
                Logger.Error(e);
                return null;
            }
        }   
    }
}
