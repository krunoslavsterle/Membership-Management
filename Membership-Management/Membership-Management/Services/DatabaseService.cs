using LiteDB;
using Membership_Management.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Membership_Management.Services
{
    public class DatabaseService
    {
        private const string DB_NAME = "membership-database.db";
        
        public void ValidateDatabaseExist()
        {
            using (var db = new LiteDatabase($"{AppDomain.CurrentDomain.BaseDirectory}{DB_NAME}"))
            {
                db.FileStorage.FindAll();
            }
        }

        public void DeleteAllCustomers()
        {
            using (var db = new LiteDatabase($"{AppDomain.CurrentDomain.BaseDirectory}{DB_NAME}"))
            {
                var customerCollection = db.GetCollection<Customer>();
                customerCollection.Delete(p => p.Id > -1);
            }
        }

        public void InsertCustomersBulk(List<Customer> customers)
        {
            using (var db = new LiteDatabase($"{AppDomain.CurrentDomain.BaseDirectory}{DB_NAME}"))
            {
                var customerCollection = db.GetCollection<Customer>();
                customerCollection.InsertBulk(customers);
            }
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            using (var db = new LiteDatabase($"{AppDomain.CurrentDomain.BaseDirectory}{DB_NAME}"))
            {
                var customerCollection = db.GetCollection<Customer>();
                return customerCollection.FindAll();
            }
        }

        public int GetCustomersCount(Expression<Func<Customer, bool>> predicate)
        {
            using (var db = new LiteDatabase($"{AppDomain.CurrentDomain.BaseDirectory}{DB_NAME}"))
            {
                var customerCollection = db.GetCollection<Customer>();

                if (predicate == null)
                    return customerCollection.Count();

                return customerCollection.Count(predicate);
            }
        }
    }
}
