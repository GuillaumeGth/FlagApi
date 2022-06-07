using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using NpgsqlTypes;
using Newtonsoft.Json;

namespace FlagApi.Models
{
    public class Message
    {
        [Key]
        [Column("id")]
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [Column("date")]
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [Column("content_id")]
        public Guid ContentId { get; set; }

        [Column("location")]
        public NpgsqlPoint Location { get; set; }

        [JsonProperty("lat")]
        [NotMapped]
        public double Latitude {get; set;}
        [JsonProperty("long")]
         [NotMapped]
        public double Longitude {get; set;}

        [Column("text")]
        [JsonProperty("content")]
        public string Text { get; set; }  
        [Column("author_id")]
        [JsonProperty("author")]
        public Guid? AuthorId { get; set; }
        [ForeignKey("AuthorId")]             
        public User Author { get; set; }
        [JsonProperty("recipient")]
        [Column("recipient_id")]
        public Guid? RecipientId { get; set; }
        [ForeignKey("RecipientId")]  
        public User Recipient { get; set; }
        [JsonProperty("seen")]
        [Column("seen")]
        public bool Seen {get; set;}
        public override string ToString()
        {
            string str = string.Empty;
            str += $@"{nameof(Id)} {Id}
";
            str += $@"{nameof(AuthorId)} {AuthorId}
";
            str += $@"{nameof(RecipientId)} {RecipientId}
";
            str += $@"{nameof(Text)} {Text}
";
            return str;
        }
    }
}