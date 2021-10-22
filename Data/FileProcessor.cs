using EcoLease_Admin.Data.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IronPdf;
using System.Drawing;
using static EcoLease_Admin.Data.UrlHelper;
using System.IO;
using static EcoLease_Admin.Data.Classes.FileAccessHelper;
using System.Net.Http.Headers;

namespace EcoLease_Admin.Data
{
    public class FileProcessor : IFileProcessor
    {
        //inserts a file to the server with binary data and filename
        public async Task<Uri> InsertFile(byte [] file, string fileName)
        {
            try
            {
                //creates multipart data with the binary param
                MultipartFormDataContent data = new MultipartFormDataContent();
                data.Add(new ByteArrayContent(file), "file", fileName);

                //sends the post request
                using(HttpResponseMessage res = await ApiHelper.ApiClient.PostAsync(FilesURL(), data))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        return res.Headers.Location;
                    }
                    else
                    {
                        throw new Exception(res.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
