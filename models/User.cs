using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace FlagApi.Models
{
    public class User
    {

        [Column("email")]
        public string Email { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }
                
        [Column("picture_url")]
        public string PictureUrl  { get; set; }
    }
}