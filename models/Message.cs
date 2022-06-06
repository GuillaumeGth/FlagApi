using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using NpgsqlTypes;

namespace FlagApi.Models
{
    public class Message
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("content_id")]
        public Guid ContentId { get; set; }

        [Column("location")]
        public NpgsqlPoint Location { get; set; }

        [Column("text")]
        public string Text { get; set; }  
         [Column("author_id")]
        public Guid? AuthorId { get; set; }
        [ForeignKey("AuthorId")]             
        public User Author { get; set; }
                
        [Column("recipient_id")]
        public Guid? RecipientId { get; set; }
        [ForeignKey("RecipientId")]  
        public User Recipient { get; set; }
        public override string ToString()
        {
            string str = string.Empty;
            str += $"author id: {AuthorId}";
//             str += @$"
// author: {Author?.Name}";
//              str += @$"
// recipient: {Recipient?.Name}";
            return str;
        }
    }
}