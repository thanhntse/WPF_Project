using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICustomerService
    {
        Customer CheckLogin(string email, string password);

        List<Customer> GetAll();

        Customer GetCustomerById(int id);

        void SaveCustomer(Customer customer);

        void UpdateCustomer(Customer customer);

        void DeleteCustomer(Customer customer);
    }
}
