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
    public class AgreementProcessor : IAgreementProcessor
    {
        //gets agreement filename with reservation id
        public async Task<Agreement> GetFileName(int id)
        {
            using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(AgreementsURL(id)))
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

        //inserts a new agreement
        public async Task<Uri> InsertAgreement(Agreement agreement)
        {
            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.PostAsJsonAsync(AgreementsURL(null), agreement))
                {
                    return res.Headers.Location;
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //removes agreement by id
        public async Task<Uri> RemoveAgreement(int id)
        {
            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.DeleteAsync(AgreementsURL(id)))
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
