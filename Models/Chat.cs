namespace KeepThingsAPI.Models
{
    public class Chat
    {
        public int chat_id { get; set; }
        public int sender_id { get; set; }
        public int receiver_id { get; set; }
        public string topic { get; set; }
    }
}
