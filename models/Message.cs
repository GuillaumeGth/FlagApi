using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using NpgsqlTypes;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using  Microsoft.AspNetCore.Mvc;

namespace FlagApi.Models
{
    public class Message
    {
        [Key]
        [Column("id")]        
        public Guid? Id { get; set; }
        [Column("date")]        
        public DateTime? Date { get; set; }
        [FromForm(Name = "contentRef")]
        [Column("content_ref")]        
        public string ContentRef { get; set; }
        // [Column("content_id")]
        // public Guid? ContentId { get; set; }
        [Column("location")]
        public NpgsqlPoint? Location { get; set; }        
        [FromForm(Name = "lat")]
        [NotMapped]
        public double Latitude {get; set;}        
        [FromForm(Name = "lon")]
        [NotMapped]
        public double Longitude {get; set;}
        [Column("text")]
        [FromForm(Name = "content")]
        public string Text { get; set; }  
        [Column("author_id")]        
        [FromForm(Name = "author")]
        public Guid? AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        [JsonIgnore]           
        public User Author { get; set; }
        [FromForm(Name = "recipient")]
        [Column("recipient_id")]
        public Guid? RecipientId { get; set; }
        [ForeignKey("RecipientId")]
        [JsonIgnore]
        public User Recipient { get; set; }
        // [ForeignKey("content_id")]
        // public Content Content { get; set; }        
        [FromForm(Name = "seen")]
        [Column("seen")]
        public bool Seen {get; set;}        
        public override string ToString()
        {
            string str = string.Empty;
            str += @":: Message ::
";
            str += $@"{nameof(Id)} {Id}
";
            str += $@"{nameof(AuthorId)} {AuthorId}
";
            str += $@"{nameof(RecipientId)} {RecipientId}
";            
            str += $@"{nameof(Text)} {Text}
";
            str += $@"{nameof(Location)} {Location}
";
            return str;
        }

    }
}