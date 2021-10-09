using EcoLease_Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Data
{
    public interface IVehicleProcessor
    {
        Task<List<Vehicle>> LoadVehicles();
        Task<Vehicle> LoadVehicle(int id);
        Task<Uri> InsertVehicle(Vehicle vehicle);
        Task<Uri> UpdateVehicle(Vehicle vehicle);
        Task<Uri> UpdateVehicleStatus(Vehicle vehicle);
        Task<Uri> RemoveVehicle(int id);
    }
}
