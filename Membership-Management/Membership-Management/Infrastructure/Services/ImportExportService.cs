using Membership_Management.Infrastructure.Helpers;
using Membership_Management.Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Membership_Management.Services
{
    public class ImportExportService
    {
        private DatabaseService databaseService = new DatabaseService();

        public void ImportOldJSON(string path)
        {
            var json = File.ReadAllText(path);
            if (string.IsNullOrEmpty(json))
                return;

            var oldCustomers = JsonConvert.DeserializeObject<List<OldCustomer>>(json);
            if (oldCustomers == null)
                return;

            var customers = new List<Customer>();
            foreach (var old in oldCustomers)
                customers.Add(MapOldToNew(old));

            databaseService.DeleteAllCustomers();
            databaseService.InsertCustomersBulk(customers);
        }

        public void ImportJSON(string path)
        {
            var json = File.ReadAllText(path);
            if (string.IsNullOrEmpty(json))
                return;

            var customers = JsonConvert.DeserializeObject<List<Customer>>(json);
            if (customers == null)
                return;

            databaseService.DeleteAllCustomers();
            databaseService.InsertCustomersBulk(customers);
        }

        public  void ExportJSON(string path)
        {
            var customers = databaseService.GetAllCustomers();
            var json = JsonConvert.SerializeObject(customers);
            var now = DateTime.Now;

            File.WriteAllText(System.IO.Path.Combine(path, $"customers-{now.ToString("yyyyMMdd")}"), json);
        }

        private Customer MapOldToNew(OldCustomer oldCustomer)
        {
            return new Customer
            {
                Id = oldCustomer.id,
                RegNumber = oldCustomer.reg_broj,
                FirstName = oldCustomer.ime.ToPascalHRCase(),
                LastName = oldCustomer.prezime.ToPascalHRCase(),
                Address = string.IsNullOrEmpty(oldCustomer.mjesto) ? oldCustomer.adresa.ToPascalHRCase() : $"{oldCustomer.adresa.ToPascalHRCase()}, {oldCustomer.mjesto.ToPascalHRCase()}",
                Email = oldCustomer.mail.ToPascalHRCase(),
                RegDate = oldCustomer.datum_registracije,
                ValidUntil = oldCustomer.vrijeme_do
            };
        }
    }
}
