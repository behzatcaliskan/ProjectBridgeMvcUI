namespace projectbridgemvc.Models
{
    public class AppUser
    {
        public int AppUserId { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}