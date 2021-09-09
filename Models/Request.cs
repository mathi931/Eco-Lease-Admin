using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Models
{
    class Request
    {
        //create
        public Request(User user, Vehicle vehicle, string status)
        {
            User = user;
            Vehicle = vehicle;
            Status = status;
        }

        //read edit delete
        public Request(int id, User user, Vehicle vehicle, string status)
        {
            Id = id;
            User = user;
            Vehicle = vehicle;
            Status = status;
        }

        //object to string
        public override string ToString()
        {
            return $"{User} requested {Vehicle} ({Status})"; 
        }

        //props
        public int Id { get; set; }
        public User User { get; set; }
        public Vehicle Vehicle { get; set; }
        public string Status { get; set; }
    }
}
