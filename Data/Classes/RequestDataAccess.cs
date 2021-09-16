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
    class RequestDataAccess : IRequestHandler
    {
        //gets all the requests
        public List<Request> GetAll()
        {
            //query to get the request objects
            string query = @"SELECT re.rID, s.name as status, u.uID, u.firstName, u.lastName, u.dateOfBirth, v.vID, v.make, v.model, v.registered, v.plateNo, v.km, v.notes, st.name as status
                            FROM Requests re
                            LEFT JOIN Statuses st ON re.statusID = st.sID
                            INNER JOIN Users u ON re.userID = u.uID
                            INNER JOIN Vehicles v ON re.vehicleID = v.vID
                            INNER JOIN Statuses s ON v.statusID = s.sID;";

            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //runs the query with mapping: the query result contains 3 objects, with mapping it returns only the Request objects as a list what contains the other 2 objects
                    //splitOn where the other two table begins (ID`s) -> this slices the query so able to map the slices to different objects
                    var requests = connection.Query<Request, User, Vehicle, Request>(query,(request, user, vehicle) => 
                        {
                            request.User = user;
                            request.Vehicle = vehicle;
                            return request;
                        },
                        splitOn: "uID, vID")
                        .Distinct();
                    return requests.ToList();
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be read", exp);
            }
        }

        //gets request by status
        public List<Request> GetByStatus(string status)
        {
            //query to get the request object
            string query = @"SELECT re.rID, s.name as status, u.uID, u.firstName, u.lastName, u.dateOfBirth, v.vID, v.make, v.model, v.registered, v.plateNo, v.km, v.notes, st.name as status
                            FROM Requests re
                            LEFT JOIN Statuses st ON re.statusID = st.sID
                            INNER JOIN Users u ON re.userID = u.uID
                            INNER JOIN Vehicles v ON re.vehicleID = v.vID
                            INNER JOIN Statuses s ON v.statusID = s.sID";

            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //runs the query with mapping: the query result contains 3 objects, with mapping it returns only the Request objects as a list what contains the other 2 objects
                    //splitOn where the other two table begins (ID`s) -> this slices the query so able to map the slices to different objects
                    var requests = connection.Query<Request, User, Vehicle, Request>(query, (request, user, vehicle) =>
                    {
                        request.User = user;
                        request.Vehicle = vehicle;
                        return request;
                    },
                        splitOn: "uID, vID")
                        .Distinct();
                    //returns the list where the status matches
                    return requests.Where(r => r.Status == status).ToList();
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be read", exp);
            }
        }

        //removes a request
        public void Remove(Request request)
        {
            try
            {
                //query for remove a request
                string query = @"DELETE FROM Requests WHERE rID = @rid";

                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    connection.Execute(query, request);
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be update", exp);
            }
        }

        //updates a request
        public void Update(Request request)
        {
            //query for update status and vehicle id (user can not change)
            string query = @"UPDATE Requests
                            SET statusID = (SELECT sID FROM Statuses WHERE name = @status),
	                            vehicleID = @vID
                            WHERE rID = @rID;";
            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    connection.Execute(query, new { status = request.Status, vID = request.Vehicle.VId, rID = request.RId});
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
