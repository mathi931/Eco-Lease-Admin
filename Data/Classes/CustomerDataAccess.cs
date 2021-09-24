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
    class CustomerDataAccess : ICustomerHandler
    {
        //gets and returns all customers
        public List<Customer> GetAll()
        {
            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown 
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //runs and returns a list of customers from the executed query
                    return connection.Query<Customer>($"SELECT * FROM Customers").ToList();
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be read", exp);
            }
        }

        //inserts a new customer
        public void Insert(Customer user)
        {
            try
            {
                //query for insert a new customer
                var sql = @"INSERT INTO Customers (firstName, lastName, dateOfBirth) 
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

        //removes a customer
        public void Remove(Customer user)
        {
            try
            {
                //query to remove a customer by ID
                var query = @"DELETE FROM Customers WHERE cID = @cid";
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

        //updates a customer
        public void Update(Customer user)
        {
            try
            {
                //query for update a customer by id
                var sql = @"update Customers SET firstName = @firstName, lastName = @lastName, dateOfBirth = @dateOfBirth WHERE cID = @cid";

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
