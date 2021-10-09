using EcoLease_Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Data
{
    public interface IReservationProcessor
    {
        Task<List<Reservation>> LoadReservations();
        Task<Reservation> LoadReservation(int id);
        Task<Uri> InsertReservation(Reservation reservation);
        Task<Uri> UpdateReservation(Reservation reservation);
        Task<Uri> UpdateReservationStatus(int id, string status);
        Task<Uri> RemoveReservation(int id);
    }
}
