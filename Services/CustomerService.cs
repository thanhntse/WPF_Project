using BusinessObjects.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository CustomerRepository;

        public CustomerService()
        {
            this.CustomerRepository = new CustomerRepository();
        }

        public Customer CheckLogin(string email, string password)
        {
            return this.CustomerRepository.CheckLogin(email, password);
        }

        public void DeleteCustomer(Customer customer)
        {
            this.CustomerRepository.DeleteCustomer(customer);
        }

        public List<Customer> GetAll()
        {
            return this.CustomerRepository.GetAll();
        }

        public Customer GetCustomerById(int id)
        {
            return this.CustomerRepository.GetCustomerById(id);
        }

        public void SaveCustomer(Customer customer)
        {
            this.CustomerRepository.SaveCustomer(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            this.CustomerRepository.UpdateCustomer(customer);
        }
    }
}
