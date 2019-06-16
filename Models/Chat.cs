namespace KeepThingsAPI.Models
{
    public class Chat
    {
        public int id { get; set; }
        public int sender_id { get; set; }
        public int receiver_id { get; set; }
        public string topic { get; set; }
    }
}
