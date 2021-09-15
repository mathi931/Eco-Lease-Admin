using Dapper;
using EcoLease_Admin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static EcoLease_Admin.Data.Classes.DataAccessHelper;

namespace EcoLease_Admin.Data
{
    class UserDataAccess : IUserHandler
    {
        //gets and returns all users
        public List<User> GetAll()
        {
            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown 
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //runs and returns a list of users from the executed query
                    return connection.Query<User>($"SELECT * FROM Users").ToList();
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be read", exp);
            }
        }

        //inserts a new user
        public void Insert(User user)
        {
            try
            {
                //query for insert a new user
                var sql = @"INSERT INTO Users (firstName, lastName, dateOfBirth) 
                                   VALUES(@firstName, @lastName, @dateOfBirth)";

                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //runs the query
                    connection.Execute(sql, user);
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be write", exp);
            }
        }

        //removes a user
        public void Remove(User user)
        {
            try
            {
                //query to remove a user by ID
                var query = @"DELETE FROM Users WHERE uID = @uid";
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //runs the query
                    connection.Execute(query, user);
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be remove", exp);
            }
        }

        //updates a user
        public void Update(User user)
        {
            try
            {
                //query for update a user by id
                var sql = @"update Users SET firstName = @firstName, lastName = @lastName, dateOfBirth = @dateOfBirth WHERE uID = @uid";

                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //runs the query
                    connection.Execute(sql, user);
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
