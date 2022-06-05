﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FlagApi.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Newtonsoft.Json;
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
        public ActionResult List(JsonElement arg)
        {
            try{
                string query = arg.GetProperty("query").ToString()?.ToLowerInvariant();                
                List<User> users = _context.Users
                    .Where(u => u.Name.ToLower().Contains(query) || u.Email.ToLower().Contains(query))
                    .ToList<User>();
                Logger.Log(users.Count);
                foreach(User u in users){
                    Logger.Log(u.Name);
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
        public ActionResult Get(JsonElement arg)
        {            
            User userParam = JsonConvert.DeserializeObject<User>(arg.ToString());
            Console.WriteLine(userParam.Email);                  
            bool exists = _context.Users.Any(u => u.Email == userParam.Email);
            if (!exists) {                
                Logger.Log("not exists");
                _context.Users.Add(userParam);
                _context.SaveChanges();
                Logger.Log(userParam.Id);
            }
            else{
                userParam = _context.Users.First(u => u.Email == userParam.Email);
                Logger.Log("exists");
            }
            return Ok(userParam.Id);            
        }
    }
}
