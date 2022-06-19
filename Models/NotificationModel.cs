using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace FlagApi.Models
{
    public class NotificationModel
    {
        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }
        [JsonProperty("isAndroiodDevice")]
        public bool IsAndroiodDevice { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
    }
    public class DataPayload
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("imageUrl")]
        public string Imageurl {get;set;} = $"{Path.Combine(Directory.GetCurrentDirectory(), "assets", "images")}/flag_logo_small.png";
        [JsonProperty("data")]
        public Dictionary<string, object> Data {get;set;} = new Dictionary<string, object>();
    }
    public class GoogleNotification
    {
        
        [JsonProperty("priority")]
        public string Priority { get; set; } = "high";
        [JsonProperty("data")]
        public DataPayload Data { get; set; }
        [JsonProperty("notification")]
        public DataPayload Notification { get; set; }
    }
}