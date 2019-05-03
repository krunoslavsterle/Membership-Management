using LiteDB;
using Membership_Management.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Membership_Management.Services
{
    public class DatabaseService
    {
        private const string DB_NAME = "membership-database.db";
        public string DatabasePath { get; }

        public DatabaseService()
        {
            if (string.IsNullOrEmpty(DatabasePath))
            {
                // Check One Drive dir exists.
                var oneDrivePath = Environment.GetEnvironmentVariable("OneDrive");
                var dbDirPath = String.Empty;
                if (string.IsNullOrEmpty(oneDrivePath))
                    dbDirPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Database";
                else
                    dbDirPath = $"{Environment.GetEnvironmentVariable("OneDrive")}\\Database";

                Directory.CreateDirectory(dbDirPath);
                DatabasePath = $"{dbDirPath}\\{DB_NAME}";
            }
        }
        
        public void ValidateDatabaseExist()
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                db.FileStorage.FindAll();
            }
        }

        public void DeleteAllCustomers()
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var customerCollection = db.GetCollection<Customer>();
                customerCollection.Delete(p => p.Id > -1);
            }
        }

        public void InsertCustomersBulk(List<Customer> customers)
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var customerCollection = db.GetCollection<Customer>();
                customerCollection.InsertBulk(customers);
            }
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var customerCollection = db.GetCollection<Customer>();
                return customerCollection.FindAll();
            }
        }

        public IEnumerable<Customer> FindCustomers(Expression<Func<Customer, bool>> predicate)
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var customerCollection = db.GetCollection<Customer>();

                if (predicate == null)
                    return customerCollection.FindAll();

                return customerCollection.Find(predicate);
            }
        }

        public int GetCustomersCount(Expression<Func<Customer, bool>> predicate)
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var customerCollection = db.GetCollection<Customer>();

                if (predicate == null)
                    return customerCollection.Count();

                return customerCollection.Count(predicate);
            }
        }

        public void DeleteCustomer(int id)
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var customerCollection = db.GetCollection<Customer>();
                customerCollection.Delete(p => p.Id == id);
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var customerCollection = db.GetCollection<Customer>();
                customerCollection.Update(customer);
            }
        }

        public void InsertCustomer(Customer customer)
        {
            customer.RegDate = DateTime.Now;
            using (var db = new LiteDatabase(DatabasePath))
            {
                var customerCollection = db.GetCollection<Customer>();
                var val = customerCollection.Insert(customer);
            }
        }

        public SMTPSettings GetSMTPSettings()
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var settingsCollection = db.GetCollection<SMTPSettings>();

                var settings = settingsCollection.FindAll().FirstOrDefault();
                if (settings == null)
                {
                    settings = new SMTPSettings()
                    {
                        Id = 1
                    };

                    settingsCollection.Insert(settings);
                }

                return settings;
            }
        }

        public void UpdateSMTPSettings(SMTPSettings settings)
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var settingsCollection = db.GetCollection<SMTPSettings>();
                settingsCollection.Update(settings);
            }
        }

        public MasterPassword GetMasterPassword()
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var masterPasswordCollection = db.GetCollection<MasterPassword>();
                var password = masterPasswordCollection.FindAll().FirstOrDefault();

                if (password == null)
                {
                    password = new MasterPassword
                    {
                        Id = 1,
                        Password = "1234"
                    };

                    masterPasswordCollection.Insert(password);
                }

                return password;
            }
        }

        public void UpdateMasterPassword(MasterPassword masterPassword)
        {
            using (var db = new LiteDatabase(DatabasePath))
            {
                var masterPasswordCollection = db.GetCollection<MasterPassword>();
                masterPasswordCollection.Update(masterPassword);
            }
        }
    }
}
