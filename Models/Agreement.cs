using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Models
{
    class Agreement
    {


        //create
        public Agreement(User user, Vehicle vehicle, DateTime leaseBegin, DateTime leaseLast)
        {
            User = user;
            Vehicle = vehicle;
            LeaseBegin = leaseBegin;
            LeaseLast = leaseLast;
        }

        //read edit delete
        public Agreement(int id, User user, Vehicle vehicle, DateTime leaseBegin, DateTime leaseLast)
        {
            Id = id;
            User = user;
            Vehicle = vehicle;
            LeaseBegin = leaseBegin;
            LeaseLast = leaseLast;
        }
        //Object to string
        public override string ToString()
        {
            return $"{User} rented {Vehicle} ({LeaseBegin} - {LeaseLast}) ";
        }

        //props
        public int Id { get; set; }
        public User User { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime LeaseBegin { get; set; }
        public DateTime LeaseLast { get; set; }
    }
    
}
