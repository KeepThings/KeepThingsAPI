using MySql.Data.Types;

namespace KeepThingsAPI.Models{
    public class UserItem{
        public int id { get; set; }
        public string item_name {get;set;}
        public string item_desc {get;set;}
        public int user_id {get;set;}
        public string borrower {get;set;}
        public string date_from {get;set;}
        public string date_to {get;set;}
    }
    
}