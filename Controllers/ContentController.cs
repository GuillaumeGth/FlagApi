using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FlagApi.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;
namespace FlagApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContentController : ControllerBase
    {
        private DatabaseContext _context;
        private IHttpContextAccessor _contextAccessor;   
        private readonly ILogger<UserController> _logger;               
        public ContentController(ILogger<UserController> logger, 
        DatabaseContext context, 
        IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _context = context;
            _contextAccessor = contextAccessor;            
        }
        [HttpGet]
        [Route("{id}")]
        public FileContentResult Get(Guid id)
        {            
            Content c = _context.Contents.First(c => c.Id == id);       
            if (!System.IO.File.Exists(c.ContentPath)){
                c.ContentPath = Directory.GetCurrentDirectory() + c.ContentPath;
            }
            if (System.IO.File.Exists(c.ContentPath)){
                _logger.LogInformation("file exist");
            }
            Byte[] b = System.IO.File.ReadAllBytes(c.ContentPath);
            return File(b, "image/jpeg");
        }
    }
}