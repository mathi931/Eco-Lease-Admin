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
        public string Email { get; set; }
        public string PhoneNo { get; set; }


        //create
        public Customer(string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNo)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNo = phoneNo;
        }

        //read, update, delete
        public Customer(int cid, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNo)
        {
            CId = cid;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNo = phoneNo;
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
