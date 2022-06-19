namespace FlagApi.Models
{
    public class FcmNotificationSetting
    {
        public string SenderId { get; set; }
        public string ServerKey { get; set; }
        public override string ToString()
        {
            return $@"SenderId: {SenderId}
ServerKey: {ServerKey}";    
        }
    }
}