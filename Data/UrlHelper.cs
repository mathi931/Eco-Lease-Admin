using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoLease_Admin.Data.Classes.ApiHelper;

namespace EcoLease_Admin.Data
{
    public static class UrlHelper
    {
        //URLs for AgreementProcessor
        public static string AgreementsURL(int? id)
        {
            //if id passed creates an url with the id
            if (id.HasValue)
            {
                return $"{ApiClient.BaseAddress}Agreements/{id}";
            }
            //else returns the string without id
            return $"{ApiClient.BaseAddress}Agreements";
        }

        //URLs for VehicleProcessor
        public static string VehiclesURL(int? id, bool update = false, string status = "")
        {
            //if id passed creates an url with the id
            if (id.HasValue)
            {
                //if its an update
                if (update)
                {
                    //if its a status update
                    if (status.Length > 0)
                    {
                        return $"{ApiClient.BaseAddress}Vehicles/status?id={id}";
                    }
                    //full update
                    return $"{ApiClient.BaseAddress}Vehicles/{id}";
                }
                //not an update with ID
                return $"{ApiClient.BaseAddress}Vehicles/{id}";
            }

            //else returns the string without id
            return $"{ApiClient.BaseAddress}Vehicles";
        }
        
        //URL for FileProcessor
        public static string FilesURL(string fileName = "")
        {
            //if filename passed return url with filename
            if(fileName.Length > 0)
            {
                return $"{ApiClient.BaseAddress}Files/{fileName}";
            }

            //else its an upload url
            return $"{ApiClient.BaseAddress}Files/upload";
        }

        //URLs for CustomerProcessor
        public static string CustomerURL(int? id, bool update = false)
        {
            if (id.HasValue)
            {
                //if there is passed id and its an update
                if (update)
                {
                    return $"{ApiClient.BaseAddress}Customers?id={id}";
                }
                //if its not an update
                return $"{ApiClient.BaseAddress}Customers/{id}";
            }
            //if no ID passed
            return $"{ApiClient.BaseAddress}Customers";
        }

        //URLs for ReservationProcessor
        public static string ReservationsURL(int? id, bool update = false, string status = "")
        {
            //if id passed creates an url with the id
            if (id.HasValue)
            {
                //if its an update
                if (update)
                {
                    //if its a status update
                    if(status.Length > 0)
                    {
                        return $"{ApiClient.BaseAddress}Reservations/status?id={id}";
                    }

                    //full update
                    return $"{ApiClient.BaseAddress}Reservations?id={id}";
                }
                
                //its not an update but with ID
                return $"{ApiClient.BaseAddress}Reservations/{id}";
            }

            //else returns the string without id
            return $"{ApiClient.BaseAddress}Reservations";
        }

        //URL for StatusProcessor
        public static string StatusURL()
        {
            return $"{ApiClient.BaseAddress}Statuses";
        }

    }
}
