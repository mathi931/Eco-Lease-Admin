using EcoLease_Admin.Data.Classes;
using EcoLease_Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static EcoLease_Admin.Data.UrlHelper;

namespace EcoLease_Admin.Data
{
    public class CustomerProcessor : ICustomerProcessor
    {
        //inserts a new customer
        public async Task<Uri> InsertCustomer(Customer customer)
        {
            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.PostAsJsonAsync(CustomerURL(null), customer))
                {
                    return res.Headers.Location;
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        //gets one customer by id
        public async Task<Customer> LoadCustomer(int id)
        {
            using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(CustomerURL(id)))
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

        //get all customers
        public async Task<List<Customer>> LoadCustomers()
        {
            using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(CustomerURL(null)))
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

        //removes a customer by ID
        public async Task<Uri> RemoveCustomer(int id)
        {
            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.DeleteAsync(CustomerURL(id)))
                {
                    return res.Headers.Location;
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        //updates a customer by ID
        public async Task<Uri> UpdateCustomer(Customer customer)
        {
            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.PutAsJsonAsync(CustomerURL(customer.CId, true), customer))
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
