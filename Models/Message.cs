namespace KeepThingsAPI.Models{
    public class Message
    {
        public int id {get;set;}
        public int sender_id {get;set;}
        public int receiver_id {get;set;}
        public string header {get;set;}
        public string message {get;set;}
        public string timestamp {get;set;}
    }
}