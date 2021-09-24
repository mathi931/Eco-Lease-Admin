using EcoLease_Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Data
{
    interface ICustomerHandler
    {
        //CRUD
        List<Customer> GetAll();
        void Insert(Customer customer);
        void Update(Customer customer);
        void Remove(Customer customer);
    }
}
