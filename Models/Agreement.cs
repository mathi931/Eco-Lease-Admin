using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Models
{
    class Agreement
    {
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
