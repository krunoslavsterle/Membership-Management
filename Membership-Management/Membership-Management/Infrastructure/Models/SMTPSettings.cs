namespace Membership_Management.Infrastructure.Models
{
    public class SMTPSettings
    {
        public int Id { get; set; }
        public string ServerName { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Enabled { get; set; }
    }
}
