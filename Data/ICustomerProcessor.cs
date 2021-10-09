using EcoLease_Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Data
{
    public interface ICustomerProcessor
    {
        Task<List<Customer>> LoadCustomers();
        Task<Customer> LoadCustomer(int id);
        Task<Uri> InsertCustomer(Customer customer);
        Task<Uri> UpdateCustomer(Customer customer);
        Task<Uri> RemoveCustomer(int id);
    }
}
