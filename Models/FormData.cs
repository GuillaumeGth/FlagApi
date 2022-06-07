using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json; 
namespace FlagApi{
public class FormData
      {         
        [StringLength(50, ErrorMessage = "{0} cannot be greater than {1} characters.")]
        [JsonProperty("query")] 
        public string Query { get; set; }
      }
}