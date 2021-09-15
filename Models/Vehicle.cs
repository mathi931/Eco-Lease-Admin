﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Models
{
    class Vehicle
    {
        //props
        public int VId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public DateTime Registered { get; set; }
        public string PlateNo { get; set; }
        public int Km { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }

        ////create
        //public Vehicle(string make, string model, DateTime registered, string plateNo, int km, string notes, string status)
        //{
        //    Make = make;
        //    Model = model;
        //    Registered = registered;
        //    PlateNo = plateNo;
        //    Km = km;
        //    Notes = notes;
        //    Status = status;
        //}
        //read, update, delete
        public Vehicle(int vid, string make, string model, DateTime registered, string plateNo, int km, string notes, string status)
        {
            VId = vid;
            Make = make;
            Model = model;
            Registered = registered;
            PlateNo = plateNo;
            Km = km;
            Notes = notes;
            Status = status;
        }

        //object to string
        public override string ToString()
        {
            return $"{Make} {Model} {Registered} {PlateNo} {Km}{Notes} {Status}"; 
        }

    }
}
