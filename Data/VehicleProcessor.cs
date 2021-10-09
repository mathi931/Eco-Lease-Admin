using EcoLease_Admin.Data.Classes;
using EcoLease_Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EcoLease_Admin.Data
{
    public class VehicleProcessor : IVehicleProcessor
    {
        public async Task<List<Vehicle>> LoadVehicles()
        {
            string url = "http://localhost:12506/api/Vehicles";

            using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (res.IsSuccessStatusCode)
                {
                    return await res.Content.ReadAsAsync<List<Vehicle>>();
                }
                else
                {
                    throw new Exception(res.ReasonPhrase);
                }
            }
        }

        public async Task<Vehicle> LoadVehicle(int id)
        {
            string url = $"http://localhost:12506/api/Vehicles/{id}";

            using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (res.IsSuccessStatusCode)
                {
                    return await res.Content.ReadAsAsync<Vehicle>();
                }
                else
                {
                    throw new Exception(res.ReasonPhrase);
                }
            }
        }

        public async Task<Uri> InsertVehicle(Vehicle vehicle)
        {
            string url = @"http://localhost:12506/api/Vehicles";
            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.PostAsJsonAsync(url, vehicle))
                {
                    return res.Headers.Location;
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task<Uri> UpdateVehicle(Vehicle vehicle)
        {
            string url = $"http://localhost:12506/api/Vehicles?id={vehicle.VId}";

            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.PutAsJsonAsync(url, vehicle))
                {
                    return res.Headers.Location;
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Uri> UpdateVehicleStatus(Vehicle vehicle)
        {
            string url = $"http://localhost:12506/api/Vehicles/status?id={vehicle.VId}";

            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.PutAsJsonAsync(url, vehicle))
                {
                    return res.Headers.Location;
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Uri> RemoveVehicle(int id)
        {
            string url = $"http://localhost:12506/api/Vehicles/{id}";

            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.DeleteAsync(url))
                {
                    return res.Headers.Location;
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
