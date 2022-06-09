using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json; 
using  Microsoft.AspNetCore.Mvc;
namespace FlagApi{
public class FormData
      { 
        public string Query { get; set; }        
      }
}