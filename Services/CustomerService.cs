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
        private readonly ICustomerRepository iCustomerRepository;

        public CustomerService()
        {
            this.iCustomerRepository = new CustomerRepository();
        }

        public Customer CheckLogin(string email, string password)
        {
            return this.iCustomerRepository.CheckLogin(email, password);
        }

        public void DeleteCustomer(Customer customer)
        {
            this.iCustomerRepository.DeleteCustomer(customer);
        }

        public List<Customer> GetAll()
        {
            return this.iCustomerRepository.GetAll();
        }

        public Customer GetCustomerById(int id)
        {
            return this.iCustomerRepository.GetCustomerById(id);
        }

        public void SaveCustomer(Customer customer)
        {
            this.iCustomerRepository.SaveCustomer(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            this.iCustomerRepository.UpdateCustomer(customer);
        }
    }
}
