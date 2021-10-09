using EcoLease_Admin.Data.Classes;
using EcoLease_Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Data
{
    public class CustomerProcessor : ICustomerProcessor
    {
        public async Task<Uri> InsertCustomer(Customer customer)
        {
            string url = @"http://localhost:12506/api/Customers";
            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.PostAsJsonAsync(url, customer))
                {
                    return res.Headers.Location;
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Customer> LoadCustomer(int id)
        {
            string url = $"http://localhost:12506/api/Customers/{id}";

            using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (res.IsSuccessStatusCode)
                {
                    return await res.Content.ReadAsAsync<Customer>();
                }
                else
                {
                    throw new Exception(res.ReasonPhrase);
                }
            }
        }

        public async Task<List<Customer>> LoadCustomers()
        {
            string url = "http://localhost:12506/api/Customers";

            using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (res.IsSuccessStatusCode)
                {
                    return await res.Content.ReadAsAsync<List<Customer>>();
                }
                else
                {
                    throw new Exception(res.ReasonPhrase);
                }
            }
        }

        public async Task<Uri> RemoveCustomer(int id)
        {
            string url = $"http://localhost:12506/api/Customers/{id}";

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

        public async Task<Uri> UpdateCustomer(Customer customer)
        {
            string url = $"http://localhost:12506/api/Customers?id={customer.CId}";

            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.PutAsJsonAsync(url, customer))
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
