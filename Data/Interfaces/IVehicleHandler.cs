using EcoLease_Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Data
{
    interface IVehicleHandler
    {
        List<Vehicle> GetAll();
        Vehicle GetByID(int id);
        void Insert(Vehicle vehicle);
        void Update(Vehicle vehicle);
        
        void UpdateStatus(Vehicle vehicle);
        void Remove(int id);
    }
}
