using System;
using System.Configuration;
using System.IO;

namespace EcoLease_Admin.Data.Classes
{
    public static class DataAccessHelper
    {
        public static string Resources()
        {
            return $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}\\Resources\\";
        }
    }
}
