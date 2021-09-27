using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Models
{
    public class Vehicle
    {
        //props
        public int VId { get; set; }
        public string Make
        {
            get { return _make; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _make = value;
                }
                else
                {
                    throw new Exception("The Make field can not be empty!");
                }
            }
        }
        public string Model { get; set; }
        public int Registered { get; set; }
        public string PlateNo { get; set; }
        public int Km { get; set; }
        public string Notes { get; set; }
        public string Img { get; set; }
        public string Status { get; set; }
        public int Price { get; set; }

        //create
        public Vehicle(string make, string model, int registered, string plateNo, int km, string notes, string status, string img, int price)
        {
            Make = make;
            Model = model;
            Registered = registered;
            PlateNo = plateNo;
            Km = km;
            Notes = notes;
            Status = status;
            Img = img;
            Price = price;
        }


        //read, update, delete
        public Vehicle(int vid, string make, string model, int registered, string plateNo, int km, string notes, string status, string img, int price)
        {
            VId = vid;
            Make = make;
            Model = model;
            Registered = registered;
            PlateNo = plateNo;
            Km = km;
            Notes = notes;
            Status = status;
            Img = img;
            Price = price;
        }
        public Vehicle()
        {

        }

        //object to string
        public override string ToString()
        {
            return $" {VId} - {Make} {Model}"; 
        }

        //private int _vID;
        private string _make;
        //private string _model;
        //private int _registered;
        //private string _plateNo;
        //private int _km;
        //private string _notes;
        //private string _status;
        //private string _img;
    }
}
