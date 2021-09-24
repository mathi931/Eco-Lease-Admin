using EcoLease_Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Data
{
    interface IReservationHandler
    {   //CRUD
        List<Reservation> GetAll();
        Reservation GetByID(int id);
        void Insert(Reservation agreement);
        void Update(Reservation agreement);
        void Remove(Reservation agreement);
    }
}
