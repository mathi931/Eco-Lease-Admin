using EcoLease_Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Data
{
    interface IUserHandler
    {
        //CRUD
        List<User> GetAll();
        void Insert(User user);
        void Update(User user);
        void Remove(User user);
    }
}
