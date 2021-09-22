using Dapper;
using EcoLease_Admin.Data.Interfaces;
using EcoLease_Admin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoLease_Admin.Data.Classes.DataAccessHelper;

namespace EcoLease_Admin.Data.Classes
{
    class StatusDataAccess : IStatusHandler
    {
        public List<Status> GetAll()
        {
            //sql query for get all
            string query = @"SELECT * FROM Statuses";
            try
            {
                //open connection in try-catch with DataAccesHelper class to avoid connection string to be shown
                using (IDbConnection connection = new SqlConnection(ConString("EcoLeaseDB")))
                {
                    //returns the List of statuses
                    return connection.Query<Status>(query).ToList();
                }
            }
            catch (SqlException exp)
            {
                //throws an error if the data access is unsucsessfull
                throw new InvalidOperationException("Data could not be read", exp);
            }
        }
    }
}
