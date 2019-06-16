namespace KeepThingsAPI.Models{
    public class Message
    {
        public int id {get;set;}
        public int chat_id { get; set; }      
        public string message {get;set;}
        public long sender_id { get; set; }
        public long sent_timestamp {get;set;}
    }
}