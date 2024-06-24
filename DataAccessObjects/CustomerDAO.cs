using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class CustomerDAO
    {
        private static CustomerDAO instance = null!;
        private static object lockObject = new object();

        private CustomerDAO() { }

        public static CustomerDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CustomerDAO();
                }
                return instance;
            }
        }

        public Customer CheckLogin(string email, string password)
        {
            using var db = new FuminiHotelManagementContext();
            return db.Customers.SingleOrDefault(c => c.EmailAddress.Equals(email)
            && c.Password.Equals(password));
        }

        public List<Customer> GetAll()
        {
            using var db = new FuminiHotelManagementContext();
            return db.Customers.ToList();
        }

        public Customer GetCustomerById(int id)
        {
            using var db = new FuminiHotelManagementContext();
            return db.Customers.FirstOrDefault(c => c.CustomerId.Equals(id));
        }

        public void SaveCustomer(Customer customer)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.Customers.Add(customer);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.Entry<Customer>(customer).State
                    = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteCustomer(Customer customer)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                var p1 = db.Customers.SingleOrDefault(c => c.CustomerId == customer.CustomerId);
                db.Customers.Remove(p1);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
