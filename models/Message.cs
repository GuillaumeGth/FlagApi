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
        public Guid Author { get; set; }

        [Column("destinator_id")]
        public Guid Destinator { get; set; }



    }
}