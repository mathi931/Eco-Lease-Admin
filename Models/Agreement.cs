using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Models
{
    public class Agreement
    {
        public Agreement() { }
        public Agreement(int aId, DateTime leaseBegin, DateTime leaseLast, string status, User user, Vehicle vehicle)
        {
            AId = aId;
            LeaseBegin = leaseBegin;
            LeaseLast = leaseLast;
            Status = status;
            User = user;
            Vehicle = vehicle;
        }
        public Agreement(DateTime leaseBegin, DateTime leaseLast, string status, User user, Vehicle vehicle)
        {
            LeaseBegin = leaseBegin;
            LeaseLast = leaseLast;
            Status = status;
            User = user;
            Vehicle = vehicle;
        }

        //Object to string
        public override string ToString()
        {
            return $"{User} rented {Vehicle} ({LeaseBegin} - {LeaseLast}) ";
        }

        //props
        public int AId { get; set; }
        public DateTime LeaseBegin { get; set; }
        public DateTime LeaseLast { get; set; }
        public string Status { get; set; }
        public User User { get; set; }
        public Vehicle Vehicle { get; set; }
    }
    
}
