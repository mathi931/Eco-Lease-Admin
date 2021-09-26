using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IronPdf;
using static EcoLease_Admin.Data.Classes.DataAccessHelper;

namespace EcoLease_Admin.Models
{
    public class Agreement
    {
        private string _aid;
        private string htmlPath = $"{LocalHDDPath()}agreement.html";

        public string AID { get => _aid; set => _aid = generateID(); }
        public PdfDocument PDF { get; set; }
        public Reservation Reservation { get; set; }

        public Agreement(Reservation reservation)
        {
            Reservation = reservation;
            PDF = AgreementPDF();
        }

        public PdfDocument AgreementPDF()
        {
            var htmlToPdf = new HtmlToPdf();
            htmlToPdf.PrintOptions.Footer.FontFamily = "Arial";
            htmlToPdf.PrintOptions.Footer.FontSize = 11;
            htmlToPdf.PrintOptions.Footer.LeftText = "Eco Lease";
            htmlToPdf.PrintOptions.Footer.RightText = "{page}";


            var pdfDoc = htmlToPdf.RenderHtmlAsPdf(editHTML());
            pdfDoc.SaveAs($"{LocalHDDPath()}{generateFileName()}");
            return pdfDoc;
        }
        private string generateFileName()
        {
            return $"AGR{Reservation.RId}.pdf";
        }
        private string generateID()
        {
            return $"AGR{Reservation.RId}";
        }
        
        private string editHTML()
        {
            //reads the file by path
            var html = File.ReadAllLines(htmlPath);


            //loops through the lines and depends on the id changes the innerHTML
            for (int i = 0; i < html.Length; i++)
            {

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
                    html[i] = Regex.Replace(html[i], @"\breplace\b", "REPLACED");
                }
                else if (html[i].Contains("email"))
                {
                    html[i] = Regex.Replace(html[i], @"\breplace\b", "REPLACED");
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
                    html[i] = Regex.Replace(html[i], @"\breplace\b", "REPLACED");
                }
                else if (html[i].Contains("costFull"))
                {
                    html[i] = Regex.Replace(html[i], @"\breplace\b", "REPLACED");
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
            return string.Join("\n", html);
        }
    }
}
