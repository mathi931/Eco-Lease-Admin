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
    public class ReservationProcessor : IReservationProcessor

    {
        //inserts a new reservation
        public async Task<Uri> InsertReservation(Reservation reservation)
        {
            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.PostAsJsonAsync(ReservationsURL(null), reservation))
                {
                    return res.Headers.Location;
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        //gets a reservation by ID
        public async Task<Reservation> LoadReservation(int id)
        {
            using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(ReservationsURL(id)))
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

        //gets all the reservations
        public async Task<List<Reservation>> LoadReservations()
        {
            using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(ReservationsURL(null)))
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

        //removes reservation by ID
        public async Task<Uri> RemoveReservation(int id)
        {
            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.DeleteAsync(ReservationsURL(id)))
                {
                    return res.Headers.Location;
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        //updates reservation by ID
        public async Task<Uri> UpdateReservation(Reservation reservation)
        {
            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.PutAsJsonAsync(ReservationsURL(reservation.RId, true), reservation))
                {
                    return res.Headers.Location;
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        //updates a reservations status by ID
        public async Task<Uri> UpdateReservationStatus(int id, string status)
        {
            string url = $"http://localhost:12506/api/Reservations/status?id={id}";

            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.PutAsJsonAsync(ReservationsURL(id, true, status), status))
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
