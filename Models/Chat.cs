namespace KeepThingsAPI.Models
{
    public class Chat
    {
        public int id { get; set; }
        public long sender_id { get; set; }
        public long receiver_id { get; set; }
        public string topic { get; set; }
    }
}
