using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Models
{
    public class Reservation
    {
        public Reservation() { }
        public Reservation(int id, DateTime leaseBegin, DateTime leaseLast, string status, Customer user, Vehicle vehicle)
        {
            RId = id;
            LeaseBegin = leaseBegin;
            LeaseLast = leaseLast;
            Status = status;
            Customer = user;
            Vehicle = vehicle;
        }
        public Reservation(DateTime leaseBegin, DateTime leaseLast, string status, Customer user, Vehicle vehicle)
        {
            LeaseBegin = leaseBegin;
            LeaseLast = leaseLast;
            Status = status;
            Customer = user;
            Vehicle = vehicle;
        }

        //props
        public int RId { get; set; }
        public DateTime LeaseBegin { get; set; }
        public DateTime LeaseLast { get; set; }
        public string Status { get; set; }
        public Customer Customer { get; set; }
        public Vehicle Vehicle { get; set; }
    }
    
}
