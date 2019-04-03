namespace KeepThingsAPI.Models{
    public class Messages
    {
        public int MESSAGE_ID {get;set;}
        public int SENDER_ID {get;set;}
        public int RECEIVER_ID {get;set;}
        public string HEADER {get;set;}
        public string MESSAGE {get;set;}
        public int TIMESTAMP {get;set;}
    }
}