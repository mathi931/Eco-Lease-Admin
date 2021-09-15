using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Models
{
    class User
    {
        //props
        public int UId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }























        //create
        public User(string firstName, string lastName, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }

        //read, update, delete
        public User(int uid, string firstName, string lastName, DateTime dateOfBirth)
        {
            UId = uid;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }

        public User()
        {
        }

        //object to string
        public override string ToString()
        {
            return $"{FirstName} {LastName}"; 
        }
    }
}
