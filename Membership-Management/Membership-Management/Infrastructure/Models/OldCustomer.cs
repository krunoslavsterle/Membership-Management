namespace Membership_Management.Infrastructure.Models
{
    public class OldCustomer
    {
        public int id { get; set; }
        public int reg_broj { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string mjesto { get; set; }
        public string adresa { get; set; }
        public string datum_registracije { get; set; }
        public string vrijeme_do { get; set; }
        public string mail { get; set; }
    }
}
