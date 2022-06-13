using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using NpgsqlTypes;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using  Microsoft.AspNetCore.Mvc;

namespace FlagApi.Models
{
    public enum ContentType{
        Image,
        Video
    }
    public class Content
    {
        [Key]
        [FromForm(Name = "contentId")]
        [Column("content_id")]
        public Guid? Id { get; set; }   
        [Column("content_name")]
        public string ContentName {get; set;}      
        [Column("content_path")]
        public string ContentPath {get; set;}        
        [Column("content_type")]    
        public ContentType ContentType {get; set;} 
    }
}