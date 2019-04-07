namespace KeepThingsAPI.Models{
    public class Message
    {
        public int ID {get;set;}
        public int SENDER_ID {get;set;}
        public int RECEIVER_ID {get;set;}
        public string HEADER {get;set;}
        public string MESSAGE {get;set;}
        public string TIMESTAMP {get;set;}
    }
}