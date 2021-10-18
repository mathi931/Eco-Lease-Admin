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
    public class StatusProcessor : IStatusProcessor
    {
        //gets statuses
        public async Task<List<Status>> LoadStatuses()
        { 
            using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(StatusURL()))
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
