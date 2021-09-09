using EcoLease_Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Data
{
    interface IAgreementHandler
    {   //CRUD
        List<Agreement> GetAll();
        Agreement GetByID(int id);
        void Insert(Agreement agreement);
        void Update(Agreement agreement);
        void Remove(Agreement agreement);
    }
}
