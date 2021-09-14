using System.Configuration;

namespace EcoLease_Admin.Data.Classes
{
    public static class DataAccessHelper
    {
        public static string ConString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
