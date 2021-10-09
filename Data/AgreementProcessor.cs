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
    public class AgreementProcessor : IAgreementProcessor
    {
        public async Task<Agreement> GetFileName(int id)
        {
            string url = $"http://localhost:12506/api/Agreements/{id}";

            using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (res.IsSuccessStatusCode)
                {
                    return await res.Content.ReadAsAsync<Agreement>();
                }
                else
                {
                    throw new Exception(res.ReasonPhrase);
                }
            }
        }

        public async Task<Uri> InsertAgreement(Agreement agreement)
        {
            string url = @"http://localhost:12506/api/Agreements";

            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.PostAsJsonAsync(url, agreement))
                {
                    return res.Headers.Location;
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Uri> RemoveAgreement(int id)
        {
            string url = $"http://localhost:12506/api/Agreements/{id}";

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
