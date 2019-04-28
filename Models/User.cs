namespace KeepThingsAPI.Models {
    public class User
    {
        public int user_id { get; set; }
        public string Auth0_id { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string tel_nr { get; set; }
        public string username { get; set; }
        public string type { get; set; }
        public bool verified { get; set; }
    }
        
}