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
    class VehicleDataAccess : IVehicleHandler
    {
        //gets all the vehicles
        public List<Vehicle> GetAll()
        {
            //sql query in variable
            string query = @"SELECT v.id, v.make, v.model, v.registered, v.plateNo, v.km, v.notes, s.name AS status
                             FROM Vehicles v 
                             LEFT JOIN Statuses s
                             ON v.statusID = s.id";
  
            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //returns the List of vehicles
                    return connection.Query<Vehicle>(query).ToList();

                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be read", exp);
            }
        }

        //gets one by ID
        public Vehicle GetByID(int id)
        {
            //sql query for get a vehicle by ID
            string query = @"SELECT v.id, v.make, v.model, v.registered, v.plateNo, v.km, v.notes, s.name AS status
                             FROM Vehicles v 
                             LEFT JOIN Statuses s
                             ON v.statusID = s.id
                             WHERE v.id = @id";
            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //runs the query
                    return connection.QuerySingle<Vehicle>(query, new { id = id });
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be delete", exp);
            }
        }

        //insert a new vehicle
        async public void Insert(Vehicle vehicle)
        {
            //sql query for get the status id
            string queryGetID = @"SELECT @statusID = id from Statuses WHERE name = @statusName";

            //sql query for insert the new vehicle
            string queryInsert = @"INSERT INTO Vehicles (make, model, registered, plateNo, km, notes, statusID) values(@make, @model, @registered, @plateNo, @km, @notes, @statusID)";

            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //gets and saves the id locally with async function
                    int id = await connection.ExecuteScalarAsync<int>(queryGetID, new { statusName = vehicle.Status});

                    //creates the new object with status id
                    var v = new
                    {
                        make = vehicle.Make,
                        model = vehicle.Model,
                        registered = vehicle.Registered,
                        plateNo = vehicle.PlateNo,
                        km = vehicle.Km,
                        notes = vehicle.Notes,
                        statusID = id
                    };
                    //inserts the new object into the vehicles table
                    connection.Execute(queryInsert, v);
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be write", exp);
            }
        }

        //removes a vehicle
        public void Remove(int id)
        {
            //sql query for delete vehicle by ID
            string query = @"DELETE FROM Vehicles WHERE id = @id";
            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //runs the query
                    connection.Execute(query, new { id = id});
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be delete", exp);
            }
        }

        //updates a vehicle
        async public void Update(Vehicle vehicle)
        {
            //query for get the status ID
            string queryGetID = @"SELECT id from Statuses WHERE name = @statusName";

            //query for update the vehicle
            string queryUpdate = @"UPDATE Vehicles SET make = @make, model = @model, registered = @registered, plateNo = @plateNo, km = @km, notes = @notes, statusID = @statusID WHERE id = @id";

            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //gets the id with async function and saves locally
                    int id = await connection.ExecuteScalarAsync<int>(queryGetID,  new { statusName = vehicle.Status});

                    //creates a local variable with the id
                    var v = new
                    {
                        id = vehicle.Id,
                        make = vehicle.Make,
                        model = vehicle.Model,
                        registered = vehicle.Registered,
                        plateNo = vehicle.PlateNo,
                        km = vehicle.Km,
                        notes = vehicle.Notes,
                        statusID = id
                    };

                    //runs the update query with the local variable
                    connection.Execute(queryUpdate, v);
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be update", exp);
            }

        }

        //updates a vehicles status
        async public void UpdateStatus(Vehicle vehicle)
        {
            //query for get the status ID
            string queryGetID = @"SELECT id from Statuses WHERE name = @status";
            //query for update the status
            string queryUpdate = @"UPDATE Vehicles SET statusID = @statusID WHERE id = @id";

            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //gets and saves locally the id what comes back from the async function
                    var statusID = await connection.ExecuteScalarAsync(queryGetID, new { status = vehicle.Status });

                    //runs the update with the new local variable
                    connection.Execute(queryUpdate, new { statusID = statusID, id = vehicle.Id});
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