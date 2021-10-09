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
    public class StatusProcessor : IStatusProcessor
    {
        public async Task<List<Status>> LoadStatuses()
        {
            string url = "http://localhost:12506/api/Statuses";

            using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (res.IsSuccessStatusCode)
                {
                    return await res.Content.ReadAsAsync<List<Status>>();
                }
                else
                {
                    throw new Exception(res.ReasonPhrase);
                }
            }
        }
    }
}
