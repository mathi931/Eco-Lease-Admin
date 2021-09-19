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
    class AgreementDataAccess : IAgreementHandler
    {
        //gets all the agreements
        public List<Agreement> GetAll()
        {
            //query to get the agreement objects
            string query = @"SELECT ag.aID, ag.leaseBegin, ag.leaseLast, s.name as status, u.uID, u.firstName, u.lastName, u.dateOfBirth, v.vID, v.make, v.model, v.registered, v.plateNo, v.km, v.notes, v.img, st.name as status
                            FROM Agreements ag
                            LEFT JOIN Statuses st ON ag.statusID = st.sID
                            INNER JOIN Users u ON ag.userID = u.uID
                            INNER JOIN Vehicles v ON ag.vehicleID = v.vID
                            INNER JOIN Statuses s ON v.statusID = s.sID;";

            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //runs the query with mapping: the query result contains 3 objects, with mapping it returns only the Request objects as a list what contains the other 2 objects
                    //splitOn where the other two table begins (ID`s) -> this slices the query so able to map the slices to different objects
                    var agreements = connection.Query<Agreement, User, Vehicle, Agreement>(query, (agreement, user, vehicle) =>
                    {
                        agreement.User = user;
                        agreement.Vehicle = vehicle;
                        return agreement;
                    },
                        splitOn: "uID, vID")
                        .Distinct();
                    return agreements.ToList();
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be read", exp);
            }
        }

        public Agreement GetByID(int id)
        {
            //query to get the agreement object
            string query = @"SELECT ag.aID, ag.leaseBegin, ag.leaseLast, s.name as status, u.uID, u.firstName, u.lastName, u.dateOfBirth, v.vID, v.make, v.model, v.registered, v.plateNo, v.km, v.notes, v.img, st.name as status
                            FROM Agreements ag
                            LEFT JOIN Statuses st ON ag.statusID = st.sID
                            INNER JOIN Users u ON ag.userID = u.uID
                            INNER JOIN Vehicles v ON ag.vehicleID = v.vID
                            INNER JOIN Statuses s ON v.statusID = s.sID;";

            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //runs the query with mapping: the query result contains 3 objects, with mapping it returns only the Request objects as a list what contains the other 2 objects
                    //splitOn where the other two table begins (ID`s) -> this slices the query so able to map the slices to different objects
                    var agreements = connection.Query<Agreement, User, Vehicle, Agreement>(query, (agreement, user, vehicle) =>
                    {
                        agreement.User = user;
                        agreement.Vehicle = vehicle;
                        return agreement;
                    },
                        splitOn: "uID, vID")
                        .Distinct();
                    return agreements.Where(x => x.AId == id) as Agreement;
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be read", exp);
            }
        }

        async public void Insert(Agreement agreement)
        {
            //sql query for get the status id
            string queryGetID = @"SELECT s.sID from Statuses as s WHERE s.name = @status";

            //sql query for insert the new vehicle
            string queryInsert = @"INSERT INTO Agreements (leaseBegin, leastLast, statusID, userID, vehicleID) values(@lBegin, @lLast, @statusID, @userID, @vehicleID)";

            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //gets and saves the id locally with async function
                    int sID = await connection.ExecuteScalarAsync<int>(queryGetID, new { status = agreement.Status });

                    //creates the new object with status id
                    var a = new
                    {   lBegin = agreement.LeaseBegin,
                        lLast = agreement.LeaseLast,
                        statusID = sID,
                        userID = agreement.User.UId,
                        vehicleID = agreement.Vehicle.VId
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

        public void Remove(Agreement agreement)
        {
            try
            {
                //query for remove the agreement
                string query = @"DELETE FROM Agreements WHERE aID = @aid";

                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //runs the query with the passed object
                    connection.Execute(query, agreement);
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be update", exp);
            }
        }

        public void Update(Agreement agreement)
        {
            //query for update dates, status, vehicle id (user can not change)
            string query = @"UPDATE Agreements
                            SET statusID = (SELECT sID FROM Statuses WHERE name = @status),
	                            vehicleID = @vID,
                                leaseBegin = @lBegin,
                                leaseLast = @lLast
                            WHERE aID = @aID;";
            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //runs the query with a new object what contains the needed variables
                    var a = new
                    {
                        status = agreement.Status,
                        vID = agreement.Vehicle.VId,
                        lBegin = agreement.LeaseBegin,
                        lLast = agreement.LeaseLast,
                        aID = agreement.AId
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
