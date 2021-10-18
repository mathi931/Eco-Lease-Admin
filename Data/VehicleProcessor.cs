using EcoLease_Admin.Data.Classes;
using EcoLease_Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static EcoLease_Admin.Data.UrlHelper;

namespace EcoLease_Admin.Data
{
    public class VehicleProcessor : IVehicleProcessor
    {
        //gets all vehicles
        public async Task<List<Vehicle>> LoadVehicles()
        {
            var path = VehiclesURL(null);
            using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(path))
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

        //gets a vehicle by ID
        public async Task<Vehicle> LoadVehicle(int id)
        {
            using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(VehiclesURL(id)))
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
        
        //insert new vehicle
        public async Task<Uri> InsertVehicle(Vehicle vehicle)
        {
            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.PostAsJsonAsync(VehiclesURL(null), vehicle))
                {
                    return res.Headers.Location;
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        //updates vehicle by ID
        public async Task<Uri> UpdateVehicle(Vehicle vehicle)
        {
            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.PutAsJsonAsync(VehiclesURL(vehicle.VId, true), vehicle))
                {
                    return res.Headers.Location;
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        //updates status by ID
        public async Task<Uri> UpdateVehicleStatus(Vehicle vehicle)
        {
            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.PutAsJsonAsync(VehiclesURL(vehicle.VId, true, vehicle.Status), vehicle))
                {
                    return res.Headers.Location;
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        //removes by ID
        public async Task<Uri> RemoveVehicle(int id)
        {
            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.DeleteAsync(VehiclesURL(id)))
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
