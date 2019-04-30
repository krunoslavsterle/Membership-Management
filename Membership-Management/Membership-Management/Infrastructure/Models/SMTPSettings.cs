namespace Membership_Management.Infrastructure.Models
{
    public class SMTPSettings
    {
        public int Id { get; set; }
        public string ServerName { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
