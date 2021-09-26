using System;
using System.Configuration;
using System.IO;

namespace EcoLease_Admin.Data.Classes
{
    public static class DataAccessHelper
    {
        public static string ConString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public static string LocalHDDPath()
        {
            return $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}\\Resources\\";
        }
    }
}
