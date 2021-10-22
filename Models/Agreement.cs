using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using EcoLease_Admin.Data;
using IronPdf;
using static EcoLease_Admin.Data.Classes.FileAccessHelper;
using static EcoLease_Admin.Data.UrlHelper;
using static EcoLease_Admin.Models.Operations;

namespace EcoLease_Admin.Models
{
    public class Agreement
    {
        //html template path
        private string htmlPath = $"{Resources()}agreement.html";

        //properties
        public int Aid { get; set; }
        public string FileName { get; set; }
        public Reservation Reservation { get; set; }


        public Agreement(Reservation reservation)
        {
            Reservation = reservation;
            FileName = generateFileName();
        }
        public Agreement()
        {

        }

        public Agreement(int aid, string fileName, Reservation reservation)
        {
            Aid = aid;
            FileName = fileName;
            Reservation = reservation;
        }

        public PdfDocument AgreementPDF()
        {
            //creates a pdf
            var htmlToPdf = new HtmlToPdf();
            htmlToPdf.PrintOptions.Footer.FontFamily = "Arial";
            htmlToPdf.PrintOptions.Footer.FontSize = 11;
            htmlToPdf.PrintOptions.Footer.LeftText = "Eco Lease";
            htmlToPdf.PrintOptions.Footer.RightText = "{page}";

            //returns the pdf from overwrote html template
            var pdfDoc = htmlToPdf.RenderHtmlAsPdf(getHTML());
            return pdfDoc;
        }

        //uploads the pdf to the server
        public async Task uploadPDF(PdfDocument pdf)
        {
            await new FileProcessor().InsertFile(pdf.BinaryData, generateFileName());
        }

        //generates filename for the document with the ID
        private string generateFileName()
        {
            return $"AGR{Reservation.RId}.pdf";
        }

        //overwrites the html template
        private string getHTML()
        {
            //reads the file by path
            var html = File.ReadAllLines(htmlPath);

            //loops through the lines and depends on the id changes the innerHTML
            for (int i = 0; i < html.Length; i++)
            {
                //switch (html[i])
                //{
                //    case string line when line.Contains("fullName"):
                //        return html[i] = Regex.Replace(html[i], @"\breplace\b", $"{this.Reservation.Customer.FirstName} {this.Reservation.Customer.LastName}");
                //    case string line when line.Contains("birthDate"):
                //        return html[i] = Regex.Replace(html[i], @"\breplace\b", this.Reservation.Customer.DateOfBirth.ToString());
                //    case string line when line.Contains("telNo"):
                //        return html[i] = Regex.Replace(html[i], @"\breplace\b", this.Reservation.Customer.PhoneNo.ToString());
                //    case string line when line.Contains("email"):
                //        return html[i] = Regex.Replace(html[i], @"\breplace\b", this.Reservation.Customer.Email.ToString());
                //    case string line when line.Contains("leaseFrom"):
                //        return html[i] = Regex.Replace(html[i], @"\breplace\b", this.Reservation.LeaseBegin.ToString());
                //    case string line when line.Contains("leaseUntil"):
                //        return html[i] = Regex.Replace(html[i], @"\breplace\b", this.Reservation.LeaseLast.ToString());
                //    case string line when line.Contains("costMonth"):
                //        return html[i] = Regex.Replace(html[i], @"\breplace\b", this.Reservation.Vehicle.Price.ToString());
                //    case string line when line.Contains("costFull"):
                //        return html[i] = Regex.Replace(html[i], @"\breplace\b", Convert.ToString(this.Reservation.toFullCost()));
                //    case string line when line.Contains("make"):
                //        return html[i] = Regex.Replace(html[i], @"\breplace\b", this.Reservation.Vehicle.Make);
                //    case string line when line.Contains("model"):
                //        return html[i] = Regex.Replace(html[i], @"\breplace\b", this.Reservation.Vehicle.Model);
                //    case string line when line.Contains("registered"):
                //        return html[i] = Regex.Replace(html[i], @"\breplace\b", this.Reservation.Vehicle.Registered.ToString());
                //    case string line when line.Contains("plateNo"):
                //        return html[i] = Regex.Replace(html[i], @"\breplace\b", this.Reservation.Vehicle.PlateNo);
                //    case string line when line.Contains("km"):
                //        return html[i] = Regex.Replace(html[i], @"\breplace\b", this.Reservation.Vehicle.Km.ToString());
                //}

                if (html[i].Contains("fullName"))
                {
                    html[i] = Regex.Replace(html[i], @"\breplace\b", $"{this.Reservation.Customer.FirstName} {this.Reservation.Customer.LastName}");
                }
                else if (html[i].Contains("birthDate"))
                {
                    html[i] = Regex.Replace(html[i], @"\breplace\b", this.Reservation.Customer.DateOfBirth.ToString());
                }
                else if (html[i].Contains("telNo"))
                {
                    html[i] = Regex.Replace(html[i], @"\breplace\b", this.Reservation.Customer.PhoneNo.ToString());
                }
                else if (html[i].Contains("email"))
                {
                    html[i] = Regex.Replace(html[i], @"\breplace\b", this.Reservation.Customer.Email.ToString());
                }
                else if (html[i].Contains("leaseFrom"))
                {
                    html[i] = Regex.Replace(html[i], @"\breplace\b", this.Reservation.LeaseBegin.ToString());
                }
                else if (html[i].Contains("leaseUntil"))
                {
                    html[i] = Regex.Replace(html[i], @"\breplace\b", this.Reservation.LeaseLast.ToString());
                }
                else if (html[i].Contains("costMonth"))
                {
                    html[i] = Regex.Replace(html[i], @"\breplace\b", this.Reservation.Vehicle.Price.ToString());
                }
                else if (html[i].Contains("costFull"))
                {
                    html[i] = Regex.Replace(html[i], @"\breplace\b", Convert.ToString(this.Reservation.toFullCost()));
                }
                else if (html[i].Contains("make"))
                {
                    html[i] = Regex.Replace(html[i], @"\breplace\b", this.Reservation.Vehicle.Make);
                }
                else if (html[i].Contains("model"))
                {
                    html[i] = Regex.Replace(html[i], @"\breplace\b", this.Reservation.Vehicle.Model);
                }
                else if (html[i].Contains("registered"))
                {
                    html[i] = Regex.Replace(html[i], @"\breplace\b", this.Reservation.Vehicle.Registered.ToString());
                }
                else if (html[i].Contains("plateNo"))
                {
                    html[i] = Regex.Replace(html[i], @"\breplace\b", this.Reservation.Vehicle.PlateNo.ToString());
                }
                else if (html[i].Contains("km"))
                {
                    html[i] = Regex.Replace(html[i], @"\breplace\b", this.Reservation.Vehicle.Km.ToString());
                }
            }
            string updatedHtml = string.Join("\n", html);
            //returns the overwrote string
            return updatedHtml;
        }
    }
}
