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
    public class ReservationProcessor : IReservationProcessor

    {
        public async Task<Uri> InsertReservation(Reservation reservation)
        {
            string url = @"http://localhost:12506/api/Reservations";
            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.PostAsJsonAsync(url, reservation))
                {
                    return res.Headers.Location;
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Reservation> LoadReservation(int id)
        {
            string url = $"http://localhost:12506/api/Reservations/{id}";

            using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (res.IsSuccessStatusCode)
                {
                    return await res.Content.ReadAsAsync<Reservation>();
                }
                else
                {
                    throw new Exception(res.ReasonPhrase);
                }
            }
        }

        public async Task<List<Reservation>> LoadReservations()
        {
            string url = "http://localhost:12506/api/Reservations";

            using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (res.IsSuccessStatusCode)
                {
                    return await res.Content.ReadAsAsync<List<Reservation>>();
                }
                else
                {
                    throw new Exception(res.ReasonPhrase);
                }
            }
        }

        public async Task<Uri> RemoveReservation(int id)
        {
            string url = $"http://localhost:12506/api/Reservations/{id}";

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

        public async Task<Uri> UpdateReservation(Reservation reservation)
        {
            string url = $"http://localhost:12506/api/Reservations?id={reservation.RId}";

            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.PutAsJsonAsync(url, reservation))
                {
                    return res.Headers.Location;
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Uri> UpdateReservationStatus(int id, string status)
        {
            string url = $"http://localhost:12506/api/Reservations/status?id={id}";

            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.PutAsJsonAsync(url, status))
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
