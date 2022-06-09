using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using  Microsoft.AspNetCore.Mvc;

namespace FlagApi.Models
{
    public class User
    {        
        [Column("email")]
        [FromForm]
        public string Email { get; set; }
     
        [Column("name")]
        [FromForm]
        public string Name { get; set; }

        [Key]
        [Column("id")]        
        public Guid? Id { get; set; }
        [Column("picture_url")]
        [FromForm]
        public string PictureUrl  { get; set; }
        public List<Message> MessagesSent {get; set;}
        public List<Message> MessagesReceived {get; set;}
        public override string ToString()
        {
            string str = string.Empty;
            str += @":: User ::
";
            str += $@"{nameof(Name)} {Name}
";
            str += $@"{nameof(Id)} {Id}
";
            str += $@"{nameof(Email)} {Email}
";
            str += $@"{nameof(PictureUrl)} {PictureUrl}
";
            return str;
        }
    }
}