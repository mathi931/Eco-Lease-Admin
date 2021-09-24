using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Models
{
    class Request
    {
        //props
        public int RId { get; set; }
        public DateTime LeaseBegin { get; set; }
        public DateTime LeaseLast { get; set; }
        public string Status { get; set; }
        public Customer User { get; set; }
        public Vehicle Vehicle { get; set; }

        //object to string
        public override string ToString()
        {
            return $"Request ID: {RId}. {User.FirstName} requested {Vehicle.Make} ({Status})"; 
        }
    }
}
