using BusinessObjects.Models;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public Customer CheckLogin(string email, string password)
            => CustomerDAO.Instance.CheckLogin(email, password);

        public void DeleteCustomer(Customer customer)
            => CustomerDAO.Instance.DeleteCustomer(customer);

        public List<Customer> GetAll()
            => CustomerDAO.Instance.GetAll();

        public Customer GetCustomerById(int id)
            => CustomerDAO.Instance.GetCustomerById(id);

        public void SaveCustomer(Customer customer)
            => CustomerDAO.Instance.SaveCustomer(customer);

        public void UpdateCustomer(Customer customer)
            => CustomerDAO.Instance.UpdateCustomer(customer);
    }
}
