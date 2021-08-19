using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

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
        public string Location { get; set; }

        [Column("text")]
        public string Text { get; set; }

    }
}