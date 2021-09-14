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
        public List<User> GetAll()
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    return connection.Query<User>($"SELECT * FROM Users").ToList();
                }
            }
            catch (SqlException exp)
            {
                throw new InvalidOperationException("Data could not be read", exp);
            }
        }

        public void Insert(User user)
        {
            try
            {
                var sql = @"INSERT INTO Users (firstName, lastName, dateOfBirth) 
                                   VALUES(@firstName, @lastName, @dateOfBirth)";


                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    connection.Execute(sql, user);
                }
            }
            catch (SqlException exp)
            {
                throw new InvalidOperationException("Data could not be write", exp);
            }
        }

        public void Remove(User user)
        {
            try
            {
                var sql = @"DELETE FROM Users WHERE id = @id";
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    connection.Execute(sql, user);
                }
            }
            catch (SqlException exp)
            {
                throw new InvalidOperationException("Data could not be remove", exp);
            }
        }

        public void Update(User user)
        {
            try
            {
                var sql = @"update Users SET firstName = @firstName, lastName = @lastName, dateOfBirth = @dateOfBirth WHERE id = @id";
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    connection.Execute(sql, user);
                }
            }
            catch (SqlException exp)
            {
                throw new InvalidOperationException("Data could not be update", exp);
            }
        }
    }
}
