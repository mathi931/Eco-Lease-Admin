using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Models
{
    public class Customer
    {
        //props
        public int CId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }























        //create
        public Customer(string firstName, string lastName, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }

        //read, update, delete
        public Customer(int uid, string firstName, string lastName, DateTime dateOfBirth)
        {
            CId = uid;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }

        public Customer()
        {
        }

        //object to string
        public override string ToString()
        {
            return $"{FirstName} {LastName}"; 
        }
    }
}
