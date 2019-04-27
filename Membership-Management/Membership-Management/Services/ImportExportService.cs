using Membership_Management.Infrastructure.Helpers;
using Membership_Management.Infrastructure.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Membership_Management.Services
{
    public class ImportExportService
    {
        private DatabaseService databaseService = new DatabaseService();

        public void ImportOldJSON(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            if (!File.Exists(path))
                return;

            var json = File.ReadAllText(path);
            if (string.IsNullOrEmpty(json))
                return;

            var oldCustomers = JsonConvert.DeserializeObject<List<OldCustomer>>(json);
            if (oldCustomers == null)
                return;

            var customers = new List<Customer>();
            foreach (var old in oldCustomers)
                customers.Add(MapOldToNew(old));

            databaseService.InsertCustomersBulk(customers);
        }

        private Customer MapOldToNew(OldCustomer oldCustomer)
        {
            return new Customer
            {
                Id = oldCustomer.id,
                RegNumber = oldCustomer.reg_broj,
                FirstName = oldCustomer.ime.ToPascalHRCase(),
                LastName = oldCustomer.prezime.ToPascalHRCase(),
                Address = oldCustomer.adresa.ToPascalHRCase(),
                Email = oldCustomer.mail.ToPascalHRCase(),
                RegDate = oldCustomer.datum_registracije,
                ValidUntil = oldCustomer.vrijeme_do
            };
        }
    }
}
