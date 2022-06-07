using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace FlagApi.Models
{
    public class User
    {
        [JsonProperty("email")]
        [Column("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        [Column("name")]
        public string Name { get; set; }

        [Key]
        [Column("id")]
        [JsonProperty("flagId")]
        public Guid Id { get; set; }

        [JsonProperty("photo")]        
        [Column("picture_url")]
        public string PictureUrl  { get; set; }
        public List<Message> MessagesSent {get; set;}
        public List<Message> MessagesReceived {get; set;}
        public override string ToString()
        {
            string str = string.Empty;
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