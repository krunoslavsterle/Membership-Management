using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Membership_Management.Infrastructure.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public int RegNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string RegDate { get; set; }
        public string ValidUntil { get; set; }
        public string Email { get; set; }
    }
}
