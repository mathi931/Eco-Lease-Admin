using Dapper;
using EcoLease_Admin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using static EcoLease_Admin.Data.Classes.DataAccessHelper;

namespace EcoLease_Admin.Data
{
    class ReservationDataAccess : IReservationHandler
    {
        //gets all the Reservations
        public List<Reservation> GetAll()
        {
            //query to get the reservation objects
            string query = @"SELECT re.rID, re.leaseBegin, re.leaseLast, s.name as status, c.cID, c.firstName, c.lastName, c.dateOfBirth, c.email, c.phoneNo, v.vID, v.make, v.model, v.registered, v.plateNo, v.km, v.notes, v.img, v.price, st.name as status
                            FROM Reservations re
                            LEFT JOIN Statuses s ON re.statusID = s.sID
                            INNER JOIN Customers c ON re.customerID = c.cID
                            INNER JOIN Vehicles v ON re.vehicleID = v.vID
                            INNER JOIN Statuses st ON v.statusID = st.sID;";

            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //runs the query with mapping: the query result contains 3 objects, with mapping it returns only the Request objects as a list what contains the other 2 objects
                    //splitOn where the other two table begins (ID`s) -> this slices the query so able to map the slices to different objects
                    var reservations = connection.Query<Reservation, Customer, Vehicle, Reservation>(query, (reservation, customer, vehicle) =>
                    {
                        reservation.Customer = customer;
                        reservation.Vehicle = vehicle;
                        return reservation;
                    },
                        splitOn: "cID, vID")
                        .Distinct();
                    return reservations.ToList();
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be read", exp);
            }
        }

        public Reservation GetByID(int id)
        {
            //query to get the reservation object
            string query = @"SELECT re.rID, re.leaseBegin, re.leaseLast, s.name as status, c.cID, c.firstName, c.lastName, c.dateOfBirth, c.email, c.phoneNo, v.vID, v.make, v.model, v.registered, v.plateNo, v.km, v.notes, v.img, v.price, st.name as status
                            FROM Reservations re
                            LEFT JOIN Statuses s ON re.statusID = s.sID
                            INNER JOIN Customers c ON re.customerID = c.cID
                            INNER JOIN Vehicles v ON re.vehicleID = v.vID
                            INNER JOIN Statuses st ON v.statusID = st.sID;";

            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //runs the query with mapping: the query result contains 3 objects, with mapping it returns only the Request objects as a list what contains the other 2 objects
                    //splitOn where the other two table begins (ID`s) -> this slices the query so able to map the slices to different objects
                    var reservations = connection.Query<Reservation, Customer, Vehicle, Reservation>(query, (reservation, customer, vehicle) =>
                    {
                        reservation.Customer = customer;
                        reservation.Vehicle = vehicle;
                        return reservation;
                    },
                        splitOn: "cID, vID")
                        .Distinct();
                    return reservations.Where(x => x.RId == id) as Reservation;
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be read", exp);
            }
        }

        async public void Insert(Reservation reservation)
        {
            //sql query for get the status id
            string queryGetID = @"SELECT s.sID from Statuses as s WHERE s.name = @status";

            //sql query for insert the new vehicle
            string queryInsert = @"INSERT INTO Reservations (leaseBegin, leaseLast, statusID, customerID, vehicleID) values(@lBegin, @lLast, @statusID, @customerID, @vehicleID)";

            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //gets and saves the id locally with async function
                    int sID = await connection.ExecuteScalarAsync<int>(queryGetID, new { status = reservation.Status });

                    //creates the new object with status id
                    var a = new
                    {   lBegin = reservation.LeaseBegin,
                        lLast = reservation.LeaseLast,
                        statusID = sID,
                        customerID = reservation.Customer.CId,
                        vehicleID = reservation.Vehicle.VId
                    };
                    //inserts the new object into the vehicles table
                    connection.Execute(queryInsert, a);
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be write", exp);
            }
        }

        public void Remove(Reservation reservation)
        {
            try
            {
                //query for remove the reservation
                string query = @"DELETE FROM Reservations WHERE rID = @rID";

                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //runs the query with the passed object
                    connection.Execute(query, reservation);
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be update", exp);
            }
        }

        public void Update(Reservation reservation)
        {
            //query for update dates, status, vehicle id (customer can not change)
            string query = @"UPDATE Reservations
                            SET statusID = (SELECT sID FROM Statuses WHERE name = @status),
	                            vehicleID = @vID,
                                leaseBegin = @lBegin,
                                leaseLast = @lLast
                            WHERE rID = @rID;";
            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //runs the query with a new object what contains the needed variables
                    var a = new
                    {
                        status = reservation.Status,
                        vID = reservation.Vehicle.VId,
                        lBegin = reservation.LeaseBegin,
                        lLast = reservation.LeaseLast,
                        rID = reservation.RId
                    };
                    connection.Execute(query, a);
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be update", exp);
            }
        }
    }
}
