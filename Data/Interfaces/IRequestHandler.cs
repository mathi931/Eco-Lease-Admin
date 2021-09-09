using EcoLease_Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Data
{
    interface IRequestHandler
    {
        //CRUD
        List<Request> GetAll();
        Request GetByID(int id);
        void Insert(Request request);
        void Update(Request request);
        void Remove(Request request);
    }
}
